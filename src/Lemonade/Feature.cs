﻿using System;
using System.Configuration;
using System.Dynamic;
using System.Linq.Expressions;
using Lemonade.Core;
using Lemonade.Resolvers;

namespace Lemonade
{
    public class Feature
    {
        public static Feature Switches { get; } = new Feature();

        public static IFeatureResolver Resolver
        {
            get { return _featureResolver ?? (_featureResolver = GetFeatureResolver()); }
            set { _featureResolver = value; }
        }

        public bool this[Func<dynamic, dynamic> keyFunction] => this[keyFunction(_key)];

        public bool this[string key]
        {
            get
            {
                if (_featureResolver == null) _featureResolver = GetFeatureResolver();
                return _featureResolver.Get(key);
            }
        }

        public static bool Switch<T>(Expression<Func<T, dynamic>> expression)
        {
            var uExpression = expression.Body as UnaryExpression;
            var mExpression = uExpression?.Operand as MemberExpression;

            return _featureResolver.Get(mExpression?.Member.Name);
        }

        public void Execute(string key, Action action)
        {
            if (this[key]) action.Invoke();
        }

        public void Execute(Func<dynamic, dynamic> keyFunction, Action action)
        {
            if (this[keyFunction]) action.Invoke();
        }

        private Feature()
        {
        }

        private static IFeatureResolver GetFeatureResolver()
        {
            var featureConfiguration = ConfigurationManager.GetSection("FeatureConfiguration") as FeatureConfigurationSection;
            if (featureConfiguration == null)
                return new AppConfigFeatureResolver();

            var type = Type.GetType(featureConfiguration.FeatureResolver);
            if (type != null)
                return Activator.CreateInstance(type) as IFeatureResolver;

            return new AppConfigFeatureResolver();
        }

        private class DynamicKey : DynamicObject
        {
            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                result = binder.Name;
                return true;
            }
        }

        private static IFeatureResolver _featureResolver;
        private readonly DynamicKey _key = new DynamicKey();
    }
}
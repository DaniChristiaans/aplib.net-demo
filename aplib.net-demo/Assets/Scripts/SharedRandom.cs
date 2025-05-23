// This program has been developed by students from the bachelor Computer Science at Utrecht
// University within the Software Project course.
// Copyright Utrecht University (Department of Information and Computing Sciences)

#nullable enable
using System;
using UnityRandom = UnityEngine.Random;

namespace ThreadSafeRandom
{
    /// <summary>
    /// A thread-safe random number generator.
    /// </summary>
    public static class SharedRandom
    {
        [ThreadStatic]
        private static Random? _local;

        private static readonly Random _global = new();

        private static int _seed;

        private static Random Instance
        {
            get
            {
                if (_local is not null) return _local;

                Assign();

                return _local!;
            }
        }

        private static void Assign()
        {
            lock (_global)
            {
                _seed = _global.Next();
            }

            _local = new Random(_seed);
            UnityRandom.InitState(_seed);
        }

        public static int Seed()
        {
            if (_local is not null) return _seed;

            Assign();

            return _seed;
        }

        public static void SetSeed(int seed)
        {
            lock (_global)
            {
                _seed = seed;
                if (_local == null) _local = new Random(seed);
                else
                    lock (_local)
                    {
                        _local = new Random(seed);
                    }

                UnityRandom.InitState(_seed);
            }
        }

        public static int Next() => Instance.Next();
        public static int Next(int maxValue) => Instance.Next(maxValue);
    }
}

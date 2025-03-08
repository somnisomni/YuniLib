using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Somni.YuniLib.Extensions {
    public static class RandomExtensions {
        public static TElement NextElement<TElement>(this Random random, IEnumerable<TElement> enumerable) {
            if(enumerable == null) {
                throw new ArgumentNullException(nameof(enumerable), "Enumerable is null.");
            }

            IEnumerable<TElement> elements = enumerable as TElement[] ?? enumerable.ToArray();

            if(!elements.Any()) {
                throw new InvalidOperationException("Enumerable has no elements.");
            }

            return elements.ElementAt(random.Next(elements.Count()));
        }

        public static object NextElement(this Random random, IEnumerable enumerable) {
            return NextElement(random, enumerable.Cast<object>());
        }

        public static TEnum NextEnum<TEnum>(this Random random) where TEnum : Enum {
            return NextElement(random, Enum.GetValues(typeof(TEnum)).Cast<TEnum>());
        }

        public static float NextFloat(this Random random, float start = 0.0f, float end = 1.0f) {
            return (float)random.NextDouble() * (end - start) + start;
        }

        public static Vector2 NextVector2(this Random random, float xMin = -1.0f, float xMax = 1.0f, float yMin = -1.0f,
            float yMax = 1.0f) {
            return new Vector2(
                x: random.NextFloat(xMin, xMax),
                y: random.NextFloat(yMin, yMax));
        }

        public static Vector2 NextVector2(this Random random, Vector2 xRange, Vector2 yRange) {
            return new Vector2(
                x: random.NextFloat(xRange.x, xRange.y),
                y: random.NextFloat(yRange.x, yRange.y));
        }

        public static Vector3 NextVector3(this Random random,
            float xMin = -1.0f, float xMax = 1.0f,
            float yMin = 1.0f, float yMax = 1.0f,
            float zMin = -1.0f, float zMax = 1.0f) {
            return new Vector3(
                x: random.NextFloat(xMin, xMax),
                y: random.NextFloat(yMin, yMax),
                z: random.NextFloat(zMin, zMax));
        }

        public static Vector3 NextVector3(this Random random, Vector3 xRange, Vector3 yRange, Vector3 zRange) {
            return new Vector3(
                x: random.NextFloat(xRange.x, xRange.y),
                y: random.NextFloat(yRange.x, yRange.y),
                z: random.NextFloat(zRange.x, zRange.y));
        }
    }
}

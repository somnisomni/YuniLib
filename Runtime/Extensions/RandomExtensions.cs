using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevTV.Utils
{
    public interface IPredicateEvaluator
    {
        bool? Evaluate(string predicate, string[] parameters);
    }
}
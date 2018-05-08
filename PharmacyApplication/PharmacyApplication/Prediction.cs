using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApplication
{
    //Author: Jed
    /// <summary>
    /// Holds a prediction and the anticipated variance in the prediction
    /// </summary>
    struct Prediction
    {
        private double _positiveVariance;

        private double _negativeVariance;

        private double _expected;

        public double PositiveVariance
        {
            get
            {
                return _positiveVariance;
            }
        }

        public double NegativeVariance
        {
            get
            {
                return _negativeVariance;
            }
        }

        public double ExpectedValue
        {
            get
            {
                return _expected;
            }
        }

        /// <summary>
        /// Intitialised for no variance
        /// </summary>
        /// <param name="expected"></param>
        public Prediction(double expected)
        {
            _positiveVariance = 0;

            _negativeVariance = 0;

            _expected = expected;
        }

        /// <summary>
        /// Initialise with symetric variance
        /// </summary>
        /// <param name="variance"></param>
        /// <param name="expected"></param>
        public Prediction(double variance, double expected)
        {
            _positiveVariance = Math.Abs(variance);

            _negativeVariance = -Math.Abs(variance);

            _expected = expected;
        }

        /// <summary>
        /// Initialise with asymetrical variance
        /// </summary>
        /// <param name="positiveVariance"></param>
        /// <param name="negativeVariance"></param>
        /// <param name="expected"></param>
        public Prediction(double positiveVariance, double negativeVariance, double expected)
        {
            _positiveVariance = positiveVariance;

            _negativeVariance = negativeVariance;

            _expected = expected;
        }

    }
}

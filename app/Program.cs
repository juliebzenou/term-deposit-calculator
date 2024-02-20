namespace CS_Basics {
    class Program {

        private enum InterestPaid {
            Monthly, 
            Quarterly, 
            Annually, 
            AtMaturity
        }

        private const double MIN_START_DEPOSIT = 1000;
        private const double MAX_START_DEPOSIT = 15000000;
        private const double MIN_INTEREST_RATE = 0;
        private const double MAX_INTEREST_RATE = 15;
        private const double MIN_INVESTMENT_TERM = 3;
        private const double MAX_INVESTMENT_TERM = 60;
        private const int MONTHS_IN_A_YEAR = 12;

        static void Main(string[] args) {
            Console.WriteLine("$"+ TermDepositCalculator(1000, 10f, 48, InterestPaid.Annually));
        }

        /// <summary>
        /// This method is a Term Deposit Calculator. It returns the final balance that would be in a term deposit 
        /// with the specified input.
        /// Assumptions: The acceptable range and edge cases for the input has been determined based on the behaviour 
        /// of the Bendigo Bank Deposit and Savings Calculator. See https://www.bendigobank.com.au/calculators/deposit-and-savings/
        /// </summary>
        /// <param name="startDepositAmount"> The amount the person deposits into their term deposit account. </param>
        /// <param name="interestRate"> The selected interest rate for the term deposit account. </param>
        /// <param name="investmentTerm"> How long the term deposit account is locked for in terms of months. </param>
        /// <param name="interestPaid"> How often interest is paid. This method assumes interest is re-invested 
        ///                             into the account. </param>
        /// <returns> Returns a double rounded to the nearest whole number of the final balance. </returns>
        static private double TermDepositCalculator(double startDepositAmount, double interestRate, int investmentTerm, InterestPaid interestPaid) {
            
            if (startDepositAmount < MIN_START_DEPOSIT || startDepositAmount > MAX_START_DEPOSIT) {
                Console.WriteLine("Your start deposit amount is outside the accepted range of $10 - $1,500,000.");
                Console.WriteLine("Please use a start deposit amount within the specified range.");
                return 0;
            }

            if (interestRate < MIN_INTEREST_RATE || interestRate > MAX_INTEREST_RATE) {
                Console.WriteLine("Your interest rate is outside the accepted range of 0% - 15%.");
                Console.WriteLine("Please use an interest rate within the specified range.");
                return 0;
            }

            if (investmentTerm < MIN_INVESTMENT_TERM || investmentTerm > MAX_INVESTMENT_TERM) {
                Console.WriteLine("Your investment term is outside the accepted range of 3 months - 5 years (60 months).");
                Console.WriteLine("Please use an investment term within the specified range.");
                return 0;
            }

            
            double finalBalance = startDepositAmount;
            int iterations = 0;
            double interestRatePerIteration = 0;
            interestRate = interestRate/100f;
            switch (interestPaid) {
                case InterestPaid.Monthly: 
                    iterations = investmentTerm;
                    interestRatePerIteration = interestRate/MONTHS_IN_A_YEAR;
                    break;

                case InterestPaid.Quarterly:
                    iterations = investmentTerm/3;
                    interestRatePerIteration = interestRate/4;
                    break;

                case InterestPaid.Annually: 
                    if (investmentTerm < MONTHS_IN_A_YEAR) {
                        Console.WriteLine("You have selected interest to be paid annually, but your investment term is less than a year.");
                        Console.WriteLine("Please use an investment term that is at least 12 months or change the frequency your investment is paid at.");
                        return 0;
                    }

                    iterations = investmentTerm/MONTHS_IN_A_YEAR;
                    interestRatePerIteration = interestRate;
                    break;

                case InterestPaid.AtMaturity: 
                    iterations = 1;
                    interestRatePerIteration = interestRate*(investmentTerm/MONTHS_IN_A_YEAR); 
                    break;
            }

            
            for (int i=0; i<iterations; i++) {
                finalBalance += Math.Round(finalBalance*interestRatePerIteration, 2); 
            }


            return Math.Round(finalBalance);
        }
    }
}


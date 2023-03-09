namespace LibraryApp.Class
{
    public class Score
    {
        
        public int Multiplier = 100; //Users get 100 points for getting the game right the first time
        public int Reducer = -50; //And lose points if they do not
        public int score = 0;

        public int CheckScore(int totalRight,int TotalQuestions)
        {
            //Calculating the score
            if (totalRight == TotalQuestions)
            {
                score += Multiplier * TotalQuestions;
            }
            else
            {
                score += (totalRight * Multiplier) + (Reducer * (TotalQuestions - totalRight));
            }
            return score;
        }

        public void Check()
        {
            score += Reducer;
        }
    }
}
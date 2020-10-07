using System;

namespace TrainExercise
{
	class Program
	{
		public static void Main(string[] args)
		{
			Train t = new Train();
			t.ConstructionTrain(2,4);
			t.ReservationAleatoire(6);
			t.ReserverPlaces();
			t.AfficherPlaces();
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}
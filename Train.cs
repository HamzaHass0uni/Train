using System;
using System.Collections.Generic;
using System.IO;

namespace TrainExercise
{

	public class Train
	{
		List<string> placesTrainTotal = new List<string>();
		List<string> placesTrain = new List<string>();
//		List<int> nomPlaces = new List<int>();
//		List<char> nomRangees = new List<char>();
//		List<int> nomWagon = new List<int>();
		List<string> placesReservees = new List<string>();
		Random rnd = new Random();
		int nbrePlacesParRangees;
		string nom = "";
		
		// Creation des places dans le train
		// Chaque place contient num wagon - char rangee - num place
		public void ConstructionTrain(int nbreWagon, int nbrePlacesParRangee){
			//nomRangees.Add('A');nomRangees.Add('B');nomRangees.Add('C');
			nbrePlacesParRangees = nbrePlacesParRangee;
			char[] rangees = new char[]{'A','B','C'};
			for(int i = 0; i < nbreWagon;i++){
				for(int j = 0; j < rangees.Length;j++){
				    	for(int k = 0; k < nbrePlacesParRangee;k++){
							string place = (i+1) +"-"+rangees[j]+"-"+(k+1); // 1-A-1
							placesTrainTotal.Add(place);
							placesTrain.Add(place);
				    	}
				    }
			}
		}
		public void ReservationAleatoire(int nombreDePlacesAleatoires){
			for(int i = 0; i < nombreDePlacesAleatoires;i++){
				int aleatoire = rnd.Next(placesTrain.Count);// nombre aleatoire 0,1,2...
				string placeAleatoire = placesTrain[aleatoire];
				placesReservees.Add(placeAleatoire);
				// pas oublier de retirer des places disponibles:
				placesTrain.RemoveAt(aleatoire);
			}
		}
		public void ReserverPlaces(){
			Console.WriteLine("Introduisez votre nom : ");
			nom = Console.ReadLine();
			AfficherPlaces();
			string[] choixArray = DemanderPlace();
			bool estDejaReserveOuFautif = false;
			//Si la place est deja reservee ou ne correspond pas, on recommence
			foreach(string p in choixArray){
				if(EstReserve(p) || EstFautif(p)){
					estDejaReserveOuFautif = true;
				}
			}
			if(estDejaReserveOuFautif){
				Console.WriteLine("Pas possible de réserver...recommencez!!!");
				ReserverPlaces();
			}else{
				ConfirmerReservation(choixArray);
			}
		}
		public void ConfirmerReservation(string[] choixArray){
			foreach(string choix in choixArray){
				placesReservees.Add(choix);
				placesTrain.Remove(choix);
			}
			using(StreamWriter writer = new StreamWriter("c://temp/ticket.res")){
				writer.WriteLine("Cher "+ nom + ", vous avez réservé : ");
				foreach(string choix in choixArray){
					writer.WriteLine(choix);
				}
			}
		}
		public string[] DemanderPlace(){
			Console.WriteLine("Veuillez choisir des places libres (séparées par virgule) ");
			string listeChoix = Console.ReadLine();
			string[] choixArray = listeChoix.Split(new char[]{','});
			return choixArray;
		}
		public void AfficherPlaces(){
			//int rangees = nbrePlacesParRangees;
			for(int i = 0; i < placesTrainTotal.Count; i++){
				string insert = "  ";
				if(EstReserve(placesTrainTotal[i])){
				   	insert = "* ";
				}
				Console.Write(placesTrainTotal[i] + insert);
				if((i+1)%nbrePlacesParRangees==0){
					Console.WriteLine();
				}
			}
//			foreach(string x in placesTrainTotal){
//				Console.Write(x);
//			}
		}
		public bool EstReserve(string place){
			foreach(string x in placesReservees){
				if(place == x){
					return true;
				}
			}
			return false;
		}
		public bool EstFautif(string place){
			foreach(string x in placesTrain){
				if(place == x){
					return false;
				}
			}
			return true;
		}
	}
}

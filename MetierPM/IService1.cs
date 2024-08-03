using MetierPM.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MetierPM
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        //[OperationContract]
        //List<Utilisateur> getAllUsers();

        // TODO: Add your service operations here
        [OperationContract]
        List<Expert> GetExperts(string Nom, string Prenom, string fonction);

        [OperationContract]
        Expert GetExpert(int? idExpert);

        [OperationContract]
        List<Expert> getAllExperts();

        [OperationContract]
        bool deleteExpert(int? idExpert);

        [OperationContract]
        bool updateExpert(int id,Expert expert);

        [OperationContract]
        bool addExpert(Expert expert);

        [OperationContract]
        bool addUser(Utilisateur utilisateur);

        [OperationContract]
        bool updateUser(Utilisateur utilisateur);

        [OperationContract]
        bool deleteUser(int id);

        [OperationContract]
        List<Utilisateur> getAllUsers();

        [OperationContract]
        Utilisateur GetUser(int id);

        [OperationContract]
        List<Utilisateur> GetUtilisateurs(string nom, string prenom);

        [OperationContract]
        bool AddMemoire(Memoire memoire);

        [OperationContract]
        bool UpdateMemoire(Memoire memoire);

        [OperationContract]
        bool DeleteMemoire(int id);

        [OperationContract]
        Memoire GetMemoire(int id);

        [OperationContract]
        List<Memoire> GetAllMemoires();

        [OperationContract]
        List<Memoire> SearchMemoires(string titre, string description);

        [OperationContract]
        bool AddAnneeAcademique(AnneeAcademique anneeAcademique);

        [OperationContract]
        bool UpdateAnneeAcademique(AnneeAcademique anneeAcademique);

        [OperationContract]
        bool DeleteAnneeAcademique(int id);

        [OperationContract]
        AnneeAcademique GetAnneeAcademique(int id);

        [OperationContract]
        List<AnneeAcademique> GetAllAnneeAcademiques();

        [OperationContract]
        bool AddCommentaire(Commentaire commentaire);

        [OperationContract]
        bool UpdateCommentaire(Commentaire commentaire);

        [OperationContract]
        bool DeleteCommentaire(int id);

        [OperationContract]
        Commentaire GetCommentaire(int id);

        [OperationContract]
        List<Commentaire> GetAllCommentaires();

        [OperationContract]
        bool AddFavori(Favori favori);

        [OperationContract]
        bool UpdateFavori(Favori favori);

        [OperationContract]
        bool DeleteFavori(int id);

        [OperationContract]
        Favori GetFavori(int id);

        [OperationContract]
        List<Favori> GetAllFavoris();

        // Jury operations
        [OperationContract]
        bool AddJury(Jury jury);

        [OperationContract]
        bool UpdateJury(Jury jury);

        [OperationContract]
        bool DeleteJury(int id);

        [OperationContract]
        Jury GetJury(int id);

        [OperationContract]
        List<Jury> GetAllJuries();

        // Opérations pour Lecteur
        [OperationContract]
        bool AddLecteur(Lecteur lecteur);

        [OperationContract]
        bool updateLecteur(int id, Lecteur lecteur);

        [OperationContract]
        bool DeleteLecteur(int id);

        [OperationContract]
        Lecteur GetLecteur(int id);

        [OperationContract]
        List<Lecteur> GetAllLecteurs();

        [OperationContract]
        List<Lecteur> getLecteurs(string prenom, string nom);

        // Opérations pour Like
        [OperationContract]
        bool AddLike(Like like);

        [OperationContract]
        bool UpdateLike(Like like);

        [OperationContract]
        bool DeleteLike(int id);

        [OperationContract]
        Like GetLike(int id);

        [OperationContract]
        List<Like> GetAllLikes();

        [OperationContract]
        bool AddMembreJury(MembreJury membreJury);

        [OperationContract]
        bool UpdateMembreJury(MembreJury membreJury);

        [OperationContract]
        bool DeleteMembreJury(int id);

        [OperationContract]
        List<MembreJury> GetAllMembresJury();

        [OperationContract]
        MembreJury GetMembreJury(int id);

        [OperationContract]
        List<MembreJury> SearchMembresJury(string nom, string prenom, string profession);
        [OperationContract]
        bool AddVerdict(Verdict verdict);

        [OperationContract]
        bool UpdateVerdict(Verdict verdict);

        [OperationContract]
        bool DeleteVerdict(int id);

        [OperationContract]
        List<Verdict> GetAllVerdicts();

        [OperationContract]
        Verdict GetVerdict(int id);

        [OperationContract]
        List<Verdict> SearchVerdicts(string statut, string mention);

        [OperationContract]
        bool AddVu(Vu vu);

        [OperationContract]
        bool UpdateVu(Vu vu);

        [OperationContract]
        bool DeleteVu(int id);

        [OperationContract]
        Vu GetVu(int id);

        [OperationContract]
        List<Vu> GetAllVus();

        [OperationContract]
        List<Expert> SearchExperts(string nom, string prenom);

        [OperationContract]
        List<Lecteur> SearchLecteurs(string nom, string prenom);

        [OperationContract]
        bool UserExists(string email);

        //[OperationContract]
        //List<Vu> SearchVus(DateTime? date, int? idLecteur, int? idMemoire);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}

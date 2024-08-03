using MetierPM.models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MetierPM
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        BdMemoireM1Context db = new BdMemoireM1Context();
        //public List<Utilisateur> getAllUsers()
        //{
        //    return db.utilisateurs.ToList();
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expert"></param>
        /// <returns></returns>
        public bool addExpert(Expert expert)
        {
            try
            {
                db.experts.Add(expert);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expert"></param>
        /// <returns></returns>
        public bool updateExpert(int id, Expert expert)
        {
            try
            {
                var leExpert = db.experts.Find(id); // Use the id parameter to find the expert
                if (leExpert != null)
                {
                    leExpert.Prenom = expert.Prenom;
                    leExpert.Nom = expert.Nom;
                    leExpert.Adresse = expert.Adresse;
                    leExpert.Telephone = expert.Telephone;
                    leExpert.fonction = expert.fonction;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idExpert"></param>
        /// <returns></returns>
        public bool deleteExpert(int? idExpert)
        {
            try
            {
                var leExpert = db.experts.Find(idExpert);
                if (leExpert != null)
                {
                    db.experts.Remove(leExpert);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Expert> getAllExperts()
        {
            return db.experts.ToList();

        }
        public Expert GetExpert(int? idExpert)
        {
            return db.experts.Find(idExpert);
        }

        public List<Expert> GetExperts(string Nom, string Prenom, string fonction)
        {
            var liste = db.experts.ToList();
            if (!string.IsNullOrEmpty(Nom))
            {
                liste = liste.Where(a => a.Nom.ToUpper().Contains(Nom.ToUpper())).ToList();
            }
            if (!string.IsNullOrEmpty(Prenom))
            {
                liste = liste.Where(a => a.Prenom.ToUpper().Contains(Prenom.ToUpper())).ToList();
            }
            if (!string.IsNullOrEmpty(fonction))
            {
                liste = liste.Where(a => a.fonction.ToUpper().Contains(fonction.ToUpper())).ToList();
            }
            return liste;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="utilisateur"></param>
        /// <returns></returns>
        public bool addUser(Utilisateur utilisateur)
        {
            try
            {
                db.utilisateurs.Add(utilisateur);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="utilisateur"></param>
        /// <returns></returns>
        public bool updateUser(Utilisateur utilisateur)
        {
            try
            {
                var leUser = db.utilisateurs.Find(utilisateur.Id);
                if (leUser != null)
                {
                    leUser.Prenom = utilisateur.Prenom;
                    leUser.Nom = utilisateur.Nom;
                    leUser.Adresse = utilisateur.Adresse;
                    leUser.Telephone = utilisateur.Telephone;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool deleteUser(int id)
        {
            try
            {
                var leUser = db.utilisateurs.Find(id);
                if (leUser != null)
                {
                    db.utilisateurs.Remove(leUser);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Utilisateur> getAllUsers()
        {
            try
            {
                return db.utilisateurs.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  // Utilise un vrai logger dans une application réelle
                throw;
            }
        }

        public bool UserExists(string email)
        {
            return db.utilisateurs.Any(u => u.Email == email);
        }

        public Utilisateur GetUser(int id)
        {
            return db.utilisateurs.Find(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <returns></returns>
        public List<Utilisateur> GetUtilisateurs(string nom, string prenom)
        {
            var liste = db.utilisateurs.ToList();
            if (!string.IsNullOrEmpty(nom))
            {
                liste = liste.Where(a => a.Nom.ToUpper().Contains(nom.ToUpper())).ToList();
            }
            if (!string.IsNullOrEmpty(prenom))
            {
                liste = liste.Where(a => a.Prenom.ToUpper().Contains(prenom.ToUpper())).ToList();
            }
            return liste;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memoire"></param>
        /// <returns></returns>
        public bool AddMemoire(Memoire memoire)
        {
            try
            {
                db.memoires.Add(memoire);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memoire"></param>
        /// <returns></returns>
        public bool UpdateMemoire(Memoire memoire)
        {
            try
            {
                var existingMemoire = db.memoires.Find(memoire.Id);
                if (existingMemoire != null)
                {
                    existingMemoire.Titre = memoire.Titre;
                    existingMemoire.Description = memoire.Description;
                    existingMemoire.DocumentPath = memoire.DocumentPath;
                    existingMemoire.statut = memoire.statut;
                    existingMemoire.imagePath = memoire.imagePath;
                   // existingMemoire.IdAnneeAcademique = memoire.IdAnneeAcademique;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteMemoire(int id)
        {
            try
            {
                var memoire = db.memoires.Find(id);
                if (memoire != null)
                {
                    db.memoires.Remove(memoire);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Memoire GetMemoire(int id)
        {
            return db.memoires.Find(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Memoire> GetAllMemoires()
        {
            return db.memoires.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="titre"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public List<Memoire> SearchMemoires(string titre, string description)
        {
            var memoires = db.memoires.AsQueryable();
            if (!string.IsNullOrEmpty(titre))
            {
                memoires = memoires.Where(m => m.Titre.Contains(titre));
            }
            if (!string.IsNullOrEmpty(description))
            {
                memoires = memoires.Where(m => m.Description.Contains(description));
            }
            return memoires.ToList();
        }

        public bool AddAnneeAcademique(AnneeAcademique anneeAcademique)
        {
            try
            {
                db.anneeacademiques.Add(anneeAcademique);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateAnneeAcademique(AnneeAcademique anneeAcademique)
        {
            try
            {
                var existingAnneeAcademique = db.anneeacademiques.Find(anneeAcademique.Id);
                if (existingAnneeAcademique != null)
                {
                    existingAnneeAcademique.anneeDebut = anneeAcademique.anneeDebut;
                    existingAnneeAcademique.annneeFin = anneeAcademique.annneeFin;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteAnneeAcademique(int id)
        {
            try
            {
                var anneeAcademique = db.anneeacademiques.Find(id);
                if (anneeAcademique != null)
                {
                    db.anneeacademiques.Remove(anneeAcademique);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public AnneeAcademique GetAnneeAcademique(int id)
        {
            return db.anneeacademiques.Find(id);
        }

        public List<AnneeAcademique> GetAllAnneeAcademiques()
        {
            return db.anneeacademiques.ToList();
        }

        public bool AddCommentaire(Commentaire commentaire)
        {
            try
            {
                db.commentaires.Add(commentaire);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateCommentaire(Commentaire commentaire)
        {
            try
            {
                var existingCommentaire = db.commentaires.Find(commentaire.Id);
                if (existingCommentaire != null)
                {
                    existingCommentaire.Description = commentaire.Description;
                    existingCommentaire.Date = commentaire.Date;
                    existingCommentaire.IdLecteur = commentaire.IdLecteur;
                    existingCommentaire.IdMemoire = commentaire.IdMemoire;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteCommentaire(int id)
        {
            try
            {
                var commentaire = db.commentaires.Find(id);
                if (commentaire != null)
                {
                    db.commentaires.Remove(commentaire);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Commentaire GetCommentaire(int id)
        {
            return db.commentaires.Find(id);
        }

        public List<Commentaire> GetAllCommentaires()
        {
            return db.commentaires.ToList();
        }

        public bool AddFavori(Favori favori)
        {
            try
            {
                db.favoris.Add(favori);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateFavori(Favori favori)
        {
            try
            {
                var existingFavori = db.favoris.Find(favori.id);
                if (existingFavori != null)
                {
                    existingFavori.Date = favori.Date;
                    existingFavori.IdLecteur = favori.IdLecteur;
                    existingFavori.IdMemoire = favori.IdMemoire;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteFavori(int id)
        {
            try
            {
                var favori = db.favoris.Find(id);
                if (favori != null)
                {
                    db.favoris.Remove(favori);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Favori GetFavori(int id)
        {
            return db.favoris.Find(id);
        }

        public List<Favori> GetAllFavoris()
        {
            return db.favoris.ToList();
        }

        // Jury operations
        public bool AddJury(Jury jury)
        {
            try
            {
                db.Jury.Add(jury);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateJury(Jury jury)
        {
            try
            {
                var existingJury = db.Jury.Find(jury.Id);
                if (existingJury != null)
                {
                    existingJury.Libelle = jury.Libelle;
                    existingJury.MembreJuries = jury.MembreJuries;
                    existingJury.Verdicts = jury.Verdicts;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteJury(int id)
        {
            try
            {
                var jury = db.Jury.Find(id);
                if (jury != null)
                {
                    db.Jury.Remove(jury);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Jury GetJury(int id)
        {
            return db.Jury.Find(id);
        }

        public List<Jury> GetAllJuries()
        {
            return db.Jury.ToList();
        }

        // Implémentations pour Lecteur
        public bool AddLecteur(Lecteur lecteur)
        {
            try
            {
                db.lecteurs.Add(lecteur);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool updateLecteur(int id, Lecteur lecteur)
        {
            try
            {
                var existingLecteur = db.lecteurs.Find(id); // Use the id parameter to find the lecteur
                if (existingLecteur != null)
                {
                    existingLecteur.Nom = lecteur.Nom;
                    existingLecteur.Prenom = lecteur.Prenom;
                    existingLecteur.DateNaissance = lecteur.DateNaissance;
                    existingLecteur.Sexe = lecteur.Sexe;
                    existingLecteur.Commentaires = lecteur.Commentaires;
                    existingLecteur.Likes = lecteur.Likes;
                    existingLecteur.Vus = lecteur.Vus;
                    existingLecteur.Favoris = lecteur.Favoris;
                db.SaveChanges();
                    return true;
                }
                else
                {
                    Console.WriteLine("Lecteur with ID " + id + " not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating Lecteur: " + ex.Message);
            }
            return false;
        }
        public bool DeleteLecteur(int id)
        {
            try
            {
                var lecteur = db.lecteurs.Find(id);
                if (lecteur != null)
                {
                    db.lecteurs.Remove(lecteur);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Lecteur GetLecteur(int id)
        {
            return db.lecteurs.Find(id);
        }

        public List<Lecteur> GetAllLecteurs()
        {
            return db.lecteurs.ToList();
        }
        //public ICollection<Lecteur> SearchLecteurs(string nom, string prenom)
        //{
        //    return db.lecteurs
        //             .Where(l => l.Nom.Contains(nom) || l.Prenom.Contains(prenom))
        //             .ToList();
        //}
        public List<Lecteur> getLecteurs(string prenom, string nom)
        {
            var lecteurs = db.lecteurs.ToList();
            if (!string.IsNullOrEmpty(prenom))
            {
                lecteurs = lecteurs.Where(v => v.Prenom.ToUpper().Contains(prenom.ToUpper())).ToList();
            }
            if (!string.IsNullOrEmpty(nom))
            {
                lecteurs = lecteurs.Where(v => v.Nom.ToUpper().Contains(nom.ToUpper())).ToList();
            }
            return lecteurs;
        }

        // Implémentations pour Like
        public bool AddLike(Like like)
        {
            try
            {
                db.likes.Add(like);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateLike(Like like)
        {
            try
            {
                var existingLike = db.likes.Find(like.Id);
                if (existingLike != null)
                {
                    existingLike.Date = like.Date;
                    existingLike.Lecteur = like.Lecteur;
                    existingLike.Memoire = like.Memoire;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteLike(int id)
        {
            try
            {
                var like = db.likes.Find(id);
                if (like != null)
                {
                    db.likes.Remove(like);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Like GetLike(int id)
        {
            return db.likes.Find(id);
        }

        public List<Like> GetAllLikes()
        {
            return db.likes.ToList();
        }
        public bool AddMembreJury(MembreJury membreJury)
        {
            try
            {
                db.MembreJury.Add(membreJury);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateMembreJury(MembreJury membreJury)
        {
            try
            {
                var existingMembreJury = db.MembreJury.Find(membreJury.Id);
                if (existingMembreJury != null)
                {
                    existingMembreJury.Nom = membreJury.Nom;
                    existingMembreJury.Prenom = membreJury.Prenom;
                    existingMembreJury.Profession = membreJury.Profession;
                    existingMembreJury.IdJury = membreJury.IdJury;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public bool DeleteMembreJury(int id)
        {
            try
            {
                var membreJury = db.MembreJury.Find(id);
                if (membreJury != null)
                {
                    db.MembreJury.Remove(membreJury);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public List<MembreJury> GetAllMembresJury()
        {
            return db.MembreJury.ToList();
        }

        public MembreJury GetMembreJury(int id)
        {
            return db.MembreJury.Find(id);
        }

        public List<MembreJury> SearchMembresJury(string nom, string prenom, string profession)
        {
            var membresJury = db.MembreJury.ToList();
            if (!string.IsNullOrEmpty(nom))
            {
                membresJury = membresJury.Where(m => m.Nom.ToUpper().Contains(nom.ToUpper())).ToList();
            }
            if (!string.IsNullOrEmpty(prenom))
            {
                membresJury = membresJury.Where(m => m.Prenom.ToUpper().Contains(prenom.ToUpper())).ToList();
            }
            if (!string.IsNullOrEmpty(profession))
            {
                membresJury = membresJury.Where(m => m.Profession.ToUpper().Contains(profession.ToUpper())).ToList();
            }
            return membresJury;
        }

        public bool AddVerdict(Verdict verdict)
        {
            try
            {
                db.verdicts.Add(verdict);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateVerdict(Verdict verdict)
        {
            try
            {
                var existingVerdict = db.verdicts.Find(verdict.Id);
                if (existingVerdict != null)
                {
                    existingVerdict.Note = verdict.Note;
                    existingVerdict.Mention = verdict.Mention;
                    existingVerdict.Commentaire = verdict.Commentaire;
                    existingVerdict.Statut = verdict.Statut;
                    existingVerdict.IdJury = verdict.IdJury;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public bool DeleteVerdict(int id)
        {
            try
            {
                var verdict = db.verdicts.Find(id);
                if (verdict != null)
                {
                    db.verdicts.Remove(verdict);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public List<Verdict> GetAllVerdicts()
        {
            return db.verdicts.ToList();
        }

        public Verdict GetVerdict(int id)
        {
            return db.verdicts.Find(id);
        }

        public List<Verdict> SearchVerdicts(string statut, string mention)
        {
            var verdicts = db.verdicts.ToList();
            if (!string.IsNullOrEmpty(statut))
            {
                verdicts = verdicts.Where(v => v.Statut.ToUpper().Contains(statut.ToUpper())).ToList();
            }
            if (!string.IsNullOrEmpty(mention))
            {
                verdicts = verdicts.Where(v => v.Mention.ToUpper().Contains(mention.ToUpper())).ToList();
            }
            return verdicts;
        }

        public bool AddVu(Vu vu)
        {
            try
            {
                db.vus.Add(vu);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateVu(Vu vu)
        {
            try
            {
                var existingVu = db.vus.Find(vu.id);
                if (existingVu != null)
                {
                    existingVu.Date = vu.Date;
                    existingVu.IdLecteur = vu.IdLecteur;
                    existingVu.IdMemoire = vu.IdMemoire;

                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteVu(int id)
        {
            try
            {
                var vu = db.vus.Find(id);
                if (vu != null)
                {
                    db.vus.Remove(vu);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Vu GetVu(int id)
        {
            return db.vus.Find(id);
        }

        public List<Vu> GetAllVus()
        {
            return db.vus.ToList();
        }

        // Service Method for searching Experts
        public List<Expert> SearchExperts(string nom, string prenom)
        {
            var query = db.experts.AsQueryable();

            if (!string.IsNullOrEmpty(nom))
            {
                query = query.Where(e => e.Nom.Contains(nom));
            }

            if (!string.IsNullOrEmpty(prenom))
            {
                query = query.Where(e => e.Prenom.Contains(prenom));
            }

            return query.ToList();
        }

        // Service Method for searching Lecteurs
        public List<Lecteur> SearchLecteurs(string nom, string prenom)
        {
            var query = db.lecteurs.AsQueryable();

            if (!string.IsNullOrEmpty(nom))
            {
                query = query.Where(l => l.Nom.Contains(nom));
            }

            if (!string.IsNullOrEmpty(prenom))
            {
                query = query.Where(l => l.Prenom.Contains(prenom));
            }

            return query.ToList();
        }


    }
}


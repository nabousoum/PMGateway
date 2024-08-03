
using MySql.Data.MySqlClient;
using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MetierPM.models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class BdMemoireM1Context: DbContext
    {
        public BdMemoireM1Context() : base("connMemoireM1") { }
        public DbSet<Utilisateur> utilisateurs  {  get; set; }
        public DbSet<Lecteur> lecteurs { get; set; }
        public DbSet<Expert> experts { get; set; }
        public DbSet<Memoire> memoires { get; set; }
        public DbSet<Commentaire> commentaires { get; set; }
        public DbSet<Like> likes { get; set; }
        public DbSet<Vu> vus { get; set; }
        public DbSet<Favori> favoris { get; set; }
        public DbSet<AnneeAcademique> anneeacademiques { get; set; }
        public DbSet<Verdict> verdicts { get; set; }
        public DbSet<Jury> Jury { get; set; }
        public DbSet<MembreJury> MembreJury { get; set; }
        public DbSet<Bibliothecaire> bibliothecaires { get; set; }
        public DbSet<Td_Erreur> td_Erreurs { get; set; }
    }
}
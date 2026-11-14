using System;
using System.Collections.Generic;

// 1. CLASSE ABSTRAITE UTILISATEUR
public abstract class Utilisateur
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string MotDePasse { get; set; } = string.Empty;

    public bool SeConnecter()
    {
        Console.WriteLine($"[Connexion] {Prenom} {Nom} s'est connecté avec succès.");
        return true;
    }

    public void SeDeconnecter()
    {
        Console.WriteLine($"[Déconnexion] {Prenom} {Nom} s'est déconnecté.");
    }
}

// 2. CLASSE PATIENT
public class Patient : Utilisateur
{
    public DateTime DateNaissance { get; set; }
    public string GroupeSanguin { get; set; } = string.Empty;

    public DMP? MonDMP { get; set; }
    public List<AutorisationAcces> Autorisations { get; set; } = new List<AutorisationAcces>();

    public void ConsulterDMP()
    {
        Console.WriteLine($"--- Patient {Prenom} {Nom} consulte son DMP ---");
        if (MonDMP != null)
        {
            MonDMP.AfficherDossier();
        }
        else
        {
            Console.WriteLine("Aucun DMP associé à ce patient.");
        }
    }
}

// 3. CLASSE MEDECIN
public class Medecin : Utilisateur
{
    public string Specialite { get; set; } = string.Empty;
    public string NumeroOrdre { get; set; } = string.Empty;

    public void RechercherPatient(int idPatient)
    {
        Console.WriteLine($"[Action Médecin] Dr. {Nom} recherche le patient avec l'ID : {idPatient}");
    }

    public void ConsulterDMP(Patient patient)
    {
        Console.WriteLine($"[Action Médecin] Dr. {Nom} consulte le DMP du patient {patient.Nom}.");
        patient.MonDMP?.AfficherDossier();
    }

    public void AjouterAntecedent(Antecedent antecedent, DMP dmp)
    {
        dmp.Antecedents.Add(antecedent);
        Console.WriteLine($"[Action Médecin] Antécédent '{antecedent.Libelle}' ajouté au DMP.");
    }

    public void PrescrireTraitement(Traitement traitement, DMP dmp)
    {
        dmp.Traitements.Add(traitement);
        Console.WriteLine($"[Action Médecin] Traitement '{traitement.Medicament}' prescrit.");
    }

    public void AjouterAnalyse(Analyse analyse, DMP dmp)
    {
        dmp.Analyses.Add(analyse);
        Console.WriteLine($"[Action Médecin] Résultat d'analyse '{analyse.Titre}' ajouté.");
    }

    public void MettreAJourDMP(Patient patient)
    {
        Console.WriteLine($"[Action Médecin] Mise à jour du DMP pour le patient {patient.Nom}.");
    }
}

// 4. CLASSE DMP
public class DMP
{
    public int IdDMP { get; set; }
    public DateTime DateCreation { get; set; }

    public List<Antecedent> Antecedents { get; set; } = new List<Antecedent>();
    public List<Traitement> Traitements { get; set; } = new List<Traitement>();
    public List<Analyse> Analyses { get; set; } = new List<Analyse>();

    public void AfficherDossier()
    {
        Console.WriteLine($"DMP N° {IdDMP} (Créé le {DateCreation.ToShortDateString()})");
        Console.WriteLine($"-> Nombre d'antécédents : {Antecedents.Count}");
        foreach (var a in Antecedents)
        {
            Console.WriteLine($"   * [{a.Type}] {a.Libelle} (Détecté le {a.DateDetection.ToShortDateString()})");
        }

        Console.WriteLine($"-> Nombre de traitements : {Traitements.Count}");
        foreach (var t in Traitements)
        {
            Console.WriteLine($"   * Médicament : {t.Medicament} - Posologie : {t.Posologie} ({t.Statut})");
        }

        Console.WriteLine($"-> Nombre d'analyses : {Analyses.Count}");
    }

    public void ConsulterHistorique()
    {
        Console.WriteLine("Consultation de l'historique médical complet du DMP.");
    }
}

// 5. CLASSES SECONDAIRES
public class Antecedent
{
    public int Id { get; set; }
    public string Libelle { get; set; } = string.Empty;
    public DateTime DateDetection { get; set; }
    public string Type { get; set; } = string.Empty;
}

public class Traitement
{
    public int Id { get; set; }
    public string Medicament { get; set; } = string.Empty;
    public string Posologie { get; set; } = string.Empty;
    public DateTime DateDebut { get; set; }
    public DateTime? DateFin { get; set; }
    public string Statut { get; set; } = string.Empty;
}

public class Analyse
{
    public int Id { get; set; }
    public string Titre { get; set; } = string.Empty;
    public DateTime DateAnalyse { get; set; }
    public string Laboratoire { get; set; } = string.Empty;
    public string Resultat { get; set; } = string.Empty;
}

public class AutorisationAcces
{
    public int IdAcces { get; set; }
    public DateTime DateDebut { get; set; }
    public DateTime? DateFin { get; set; }
    public string NiveauAcces { get; set; } = string.Empty;

    public void RevoquerAcces()
    {
        Console.WriteLine("Accès révoqué pour le médecin.");
    }
}

public class Laboratoire
{
    public int IdLabo { get; set; }
    public string NomLabo { get; set; } = string.Empty;
}

// 6. PROGRAMME PRINCIPAL
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== SYSTEME DE GESTION DES DOSSIERS MEDICAUX (DMP) ===\n");

        Medecin medecin = new Medecin
        {
            Id = 1,
            Nom = "Ben Salah",
            Prenom = "Mehdi",
            Email = "dr.bensalah@hopital.tn",
            MotDePasse = "med123",
            Specialite = "Cardiologie",
            NumeroOrdre = "12345"
        };

        Patient patient = new Patient
        {
            Id = 1,
            Nom = "Zhioua",
            Prenom = "Sirine",
            Email = "sirine.zhioua@enicar.tn",
            MotDePasse = "pat123",
            DateNaissance = new DateTime(2004, 5, 12),
            GroupeSanguin = "A+"
        };

        DMP dmp = new DMP
        {
            IdDMP = 1001,
            DateCreation = DateTime.Now
        };
        patient.MonDMP = dmp;

        medecin.SeConnecter();
        patient.SeConnecter();
        medecin.RechercherPatient(patient.Id);

        Antecedent ant = new Antecedent
        {
            Id = 1,
            Libelle = "Allergie à la pénicilline",
            DateDetection = new DateTime(2022, 3, 10),
            Type = "Allergie"
        };
        medecin.AjouterAntecedent(ant, dmp);

        Traitement trait = new Traitement
        {
            Id = 1,
            Medicament = "Paracétamol",
            Posologie = "1 comprimé 3 fois par jour",
            DateDebut = DateTime.Now,
            Statut = "En cours"
        };
        medecin.PrescrireTraitement(trait, dmp);

        Console.WriteLine();
        patient.ConsulterDMP();

        Console.WriteLine("\nAppuyez sur une touche pour quitter...");
        Console.ReadKey();
    }
}
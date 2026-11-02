# language: fr
Fonctionnalité: Éligibilité à la souscription d'assurance
  En tant qu'assureur
  Je veux vérifier si un prospect est éligible
  Afin de ne pas assurer de conducteurs non autorisés

  Contexte:
    Étant donné nous sommes le "01/01/2026"

  # Règle (FR-005) : un conducteur dont le permis est suspendu est refusé
  @eligibility @happy-path @smoke
  Scénario: Un assureur refuse un conducteur au permis suspendu
    Étant donné un conducteur âgé de 18 ans avec 2 ans de permis dont le permis est suspendu
    Et le véhicule est une "Voiture"
    Quand je demande une éligibilité
    Alors la demande est refusée avec le motif "Permis suspendu"

  @eligibility @edge-case
  Scénario: Un assureur refuse avec le motif permis suspendu un conducteur aussi trop jeune
    Étant donné un conducteur âgé de 17 ans dont le permis est suspendu
    Et le véhicule est une "Voiture"
    Quand je demande une éligibilité
    Alors la demande est refusée avec le motif "Permis suspendu"

  @eligibility @edge-case
  Scénario: Un assureur refuse avec le motif permis suspendu un motard sans assez d'expérience
    Étant donné un conducteur avec 2 ans de permis dont le permis est suspendu
    Et le véhicule est une "Moto" de 120 chevaux
    Quand je demande une éligibilité
    Alors la demande est refusée avec le motif "Permis suspendu"

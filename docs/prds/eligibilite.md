---
title: "MonAssurance — Éligibilité à la souscription"
status: approved
---

# MonAssurance — Éligibilité à la souscription

## Objectif

Dire à un prospect, avant tout paiement, s'il peut assurer son véhicule, et sinon pourquoi.

## Exigences fonctionnelles

### Éligibilité

* `FR-001` : un conducteur de moins de 18 ans est refusé pour une voiture ou une moto, motif « Conducteur trop jeune pour ce véhicule ».
* `FR-002` : une trottinette électrique est assurable dès 16 ans.
* `FR-003` : une moto de plus de 100 ch exige 5 ans de permis, motif « Expérience insuffisante pour la puissance ».
* `FR-004` : un conducteur ayant eu un sinistre responsable dans les 3 dernières années paie une surprime de 20 %.
* `FR-005` : un conducteur dont le permis est suspendu est refusé, motif « Permis suspendu ».

## Exigences non fonctionnelles

* `NFR-001` : la réponse d'éligibilité est rendue en moins de 300 ms au 95e centile.

## Hors périmètre

Le calcul du tarif complet et le paiement.

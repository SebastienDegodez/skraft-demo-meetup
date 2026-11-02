---
adr: 001
title: Command/Query classification of the eligibility trigger
status: Accepted
chosen: Query
decision: >
  We will classify CheckEligibility as a Query that returns the EligibilityViewModel and neither raises nor stores any domain event, the suspended-licence refusal included.
supersedes: null
date: 2026-11-02
ratified_by: "human 2026-11-02"
---

# ADR-001: Command/Query classification of the eligibility trigger

**Status:** Accepted
**Date:** 2026-11-02

## Context

The Eligibility context answers one question for a prospect before any payment: can this vehicle be insured, and if not, why (PRD `eligibilite`, Objectif). Story #2 (FR-005) adds a refusal: a driver whose licence is suspended is refused with the reason « Permis suspendu ». This is the first event model for the context, so it has to say whether its single trigger, `CheckEligibility`, is a Command or a Query.

Forces:

- **No history is asked for.** No story or acceptance criterion asks to record who was refused, to audit past answers, to query refusals over time or to react to a refusal downstream. The prospect only needs the answer on screen.
- **Nothing is persisted today.** The evaluation runs over facts the caller declares (date of birth, licence years, vehicle, and now the suspension status). It loads no stored state and has no repository and no Infrastructure implementation (research, File analysis and Key discoveries).
- **Latency budget.** NFR-001 requires the answer in under 300 ms at the 95th percentile. A pure in-memory evaluation stays well inside it. A write on every request would put a store on the critical path.
- **The answer is repeatable.** The same declared facts on the same day always give the same verdict, so there is no state change to record.

The decision point is whether a refusal becomes a recorded business fact (a Command that raises a past-tense Domain Event) or stays a computed answer (a Query).

## Decision

We will classify `CheckEligibility` as a Query. It passes the declared facts to the `EligibilityPolicy` Domain Service and returns the `EligibilityViewModel` Read Model (eligible or not, with the refusal reason). It changes no state, raises no Domain Event and persists nothing. Story #2 adds the suspended-licence refusal as one more outcome of this Query.

## Consequences

**Positive:**
- The evaluation stays stateless and side-effect free, so it can be repeated and tested without a store, and it fits the NFR-001 latency budget.
- A new refusal rule such as FR-005 changes only the domain policy and the input it reads. No event, store or transaction is added.
- The trigger keeps the CQS baseline already in the code: a query handler that the endpoint injects directly.

**Negative / trade-offs:**
- No trace remains of who was refused or why. A future story asking for an audit trail, a refusal history or a way to contest a refusal will need a new ADR that supersedes this one and turns the trigger into a Command with its Domain Event.
- The verdict depends on facts the caller declares, the suspension status included. Nothing records what was declared, so a false declaration cannot be detected or traced afterwards.

**Neutral:**
- The HTTP operation stays a `POST` because it carries a request body, although it is a read.
- The CQS baseline is unchanged. This ADR introduces no dispatch bus.

## Alternatives Rejected

| Alternative | Reason rejected |
|---|---|
| Command that raises an "eligibility decided" Domain Event and persists it | No story asks for history, audit or a downstream reaction, so the event would have no consumer. It would also bring an aggregate, a repository, a store and a transaction into a request that has none today, and would put a write on the NFR-001 critical path. |
| Query that also logs each refusal on the side | A side effect inside a query breaks the CQS baseline, and it would be an audit trail in all but name with no story asking for it. If such a trail is ever needed, it has to be a Command (first row) so that it is explicit. |

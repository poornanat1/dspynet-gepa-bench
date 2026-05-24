# Setup

## Dataset

[RealKIE](https://indicodatasolutions.github.io/RealKIE/) `s1_truncated`, downloaded from `s3://project-fruitfly/s1_truncated/` (Wasabi, no auth). Each row is a passage from an SEC S-1 filing with character-span annotations for 24 entity types (the paper specifies 21; the truncated build surfaces a few extras like `(Header) Prospectus Summary`).

- 75 train passages, 50 test passages
- Each passage capped to 8000 chars

## Task

A single `Predict<S1ExtractionSig>`.

- **Input:** passage text + comma-separated entity-type list
- **Output:** JSON object mapping each entity type to an array of extracted strings

## Metric

Per entity type, TP/FP/FN accumulated across all test docs (set-membership over extracted strings, case-insensitive after trim); micro-F1 per type, macro-averaged across the 24 types.

## GEPA

| Setting | Value |
|---|---|
| Auto budget | `Light` |
| Reflection minibatch | 3 |
| Validation split | 0.3 |
| Acceptance gate | strict improvement |
| Merge proposer | every 5 iterations |
| Seed | 42 |
| Runs per combo | 1 |

## Models

| Slug | Display |
|---|---|
| `claude-haiku-4-5` | Haiku 4.5 |
| `claude-sonnet-4-6` | Sonnet 4.6 |
| `claude-opus-4-7` | Opus 4.7 |

Six combinations = `{H, S, O}_task × {H, S, O}_reflect` minus the three cases where the reflection LM is weaker than the task LM.
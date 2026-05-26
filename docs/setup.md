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

## Providers

Any LM provider can be plugged in via the `Provider` abstraction ([`src/Bench/Provider.cs`](../src/Bench/Provider.cs)). Two are built in; add more by following the Mistral pattern (base URL + tier list + factory).

| Provider | Env key | Tiers (weak → strong) | API |
|---|---|---|---|
| Anthropic | `ANTHROPIC_API_KEY` | Haiku 4.5 → Sonnet 4.6 → Opus 4.7 | `api.anthropic.com` (direct) |
| Mistral | `MISTRAL_API_KEY` | `mistral-small-latest` → `…-medium-latest` → `…-large-latest` | `api.mistral.ai/v1` (OpenAI-compat) |

The matrix structure is fixed: six combinations = `{weak, mid, strong}_task × {weak, mid, strong}_reflect` minus the three task-stronger-than-reflect cases (no upside from a weaker reflection model).

Provider selection (in order): `--provider=<name>` CLI arg → `$BENCH_PROVIDER` → auto-pick the first provider whose env key is set in `.env`.

Output file is `results/benchmark-<provider>.md`.

## LM call config

Calls go out with provider defaults — no `temperature` or `reasoning_effort` overrides. For Mistral that means default temperature (~0.7) and, on hybrid models (Small 4 and Medium 3.5), reasoning enabled by default. Baseline-to-GEPA deltas therefore include both prompt-quality lift and any noise from sampling/reasoning variance across the 50 test docs.
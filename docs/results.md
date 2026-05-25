# Results

## Anthropic (Haiku / Sonnet / Opus)

| Task LM | Reflect LM | Baseline | GEPA | Δ (pp) |
|---|---|---:|---:|---:|
| Haiku 4.5 | Haiku 4.5 | 0.386 | 0.616 | +23.0 |
| Haiku 4.5 | Sonnet 4.6 | 0.386 | 0.622 | +23.6 |
| Haiku 4.5 | Opus 4.7 | 0.386 | 0.622 | +23.6 |
| Sonnet 4.6 | Sonnet 4.6 | 0.513 | 0.574 | +6.1 |
| Sonnet 4.6 | Opus 4.7 | 0.513 | 0.555 | +4.2 |
| Opus 4.7 | Opus 4.7 | 0.641 | 0.703 | +6.2 |

Per-entity-type tables and final GEPA-written instructions: [`../results/benchmark-anthropic.md`](../results/benchmark-anthropic.md).

### Observations (Anthropic)

**Haiku rows are clustered.** HH=0.616, HS=0.622, HO=0.622 — within 0.6 pp on 50 test docs. Reflection-model choice is not a primary driver in this slice when the task LM is Haiku.

**Sonnet/Opus < Sonnet/Sonnet.** 0.555 vs 0.574 (−1.9 pp). One-seed runs and Pareto best-on-val candidate selection both contribute uncertainty; the gap is small.

**Δ is largest where the baseline is lowest.** Haiku +23 pp, Sonnet +5–6 pp, Opus +6 pp. N=3 task LMs; suggestive, not conclusive.

**Optimized Haiku ≈ unoptimized Opus.** 0.616–0.622 vs 0.641. The optimized-Opus ceiling is 0.703.

**Three types stayed at 0.0 across every combo.** Risk Clauses, Title of Security Registered, Prospectus Summary (1st Para). All three have multi-sentence prose-block gold annotations; exact-string match against a paraphrased extraction scores zero. Metric limitation, not a model one — token-F1 or partial-match would change the numbers.

## Mistral (Small / Medium / Large)

_Full 6-combo run in progress. Numbers will land in [`../results/benchmark-mistral.md`](../results/benchmark-mistral.md) and be summarized here when the run completes._

## Caveats

- Single seed, 50 test docs, 6 combos per provider. No confidence intervals.
- `s1_truncated` is a derivative of RealKIE — chunked passages, not full documents. Numbers are not comparable to the RealKIE paper baselines.
- Exact-string match. See the three unscoreable types above.
- Wall-clock and dollar cost not measured per combo.
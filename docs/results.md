# Results

## Anthropic (Haiku 4.5 / Sonnet 4.6 / Opus 4.7)

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

**Haiku rows are clustered.** HH=0.616, HS=0.622, HO=0.622 — within 0.6 pp on 50 test docs. Reflection-model choice is not a primary driver when the task LM is Haiku.

**Sonnet/Opus < Sonnet/Sonnet.** 0.555 vs 0.574 (−1.9 pp). One-seed runs and Pareto best-on-val candidate selection both contribute uncertainty; the gap is small.

**Δ is largest where the baseline is lowest.** Haiku +23 pp, Sonnet +5–6 pp, Opus +6 pp.

**Optimized Haiku ≈ unoptimized Opus.** 0.616–0.622 vs 0.641. The optimized-Opus ceiling is 0.703.

**Three types stayed at 0.0 across every combo.** Risk Clauses, Title of Security Registered, Prospectus Summary (1st Para) — multi-sentence prose-block gold; exact-string match scores zero against any paraphrase. Metric limitation.

## Mistral (Small 4 / Medium 3.5 / Large 2512)

| Task LM | Reflect LM | Baseline | GEPA | Δ (pp) |
|---|---|---:|---:|---:|
| Small 4 | Small 4 | 0.358 | 0.370 | +1.2 |
| Small 4 | Medium 3.5 | 0.358 | 0.528 | **+17.0** |
| Small 4 | Large 2512 | 0.358 | 0.458 | +10.0 |
| Medium 3.5 | Medium 3.5 | 0.370 | 0.461 | +9.1 |
| Medium 3.5 | Large 2512 | 0.370 | 0.468 | +9.8 |
| Large 2512 | Large 2512 | 0.396 | **0.538** | +14.2 |

Per-entity-type tables and final GEPA-written instructions: [`../results/benchmark-mistral.md`](../results/benchmark-mistral.md).

### Observations (Mistral)

**Mid-tier reflection beats top-tier for Small tasks.** Small/Medium = 0.528 vs Small/Large = 0.458. On Anthropic the Haiku rows were flat across reflection tiers (0.616–0.622); here Medium-as-reflector is a clear local optimum. Possible explanation: Large writes instructions too dense for Small to execute reliably; Medium's prompts are simpler and Small follows them better.

**Same-tier reflection floor.** Small/Small only gained +1.2 pp. Reflection model needs to be at least one tier above task to extract anything.

**Top-tier self-reflection is the strongest combo.** Large/Large gained +14.2 pp, more than double Anthropic's Opus/Opus (+6.2 pp). The lower Mistral Large baseline (0.396 vs Opus 0.641) leaves more headroom for prompt optimization.

**Mistral baselines are tightly clustered.** 0.358 / 0.370 / 0.396 — only 3.8 pp from Small to Large. Anthropic spanned 0.386 → 0.641 (+25.5 pp). On RealKIE S-1 specifically, the tier gap inside Mistral is small.

**Best Mistral result (Large/Large = 0.538) is below optimized Anthropic Haiku (0.616–0.622).** Different model families, different floors.

## Caveats

- Single seed (42), 50 test docs, 6 combos per provider. No confidence intervals.
- `s1_truncated` is a derivative of RealKIE — chunked passages, not full documents. Numbers are not comparable to the RealKIE paper baselines.
- Exact-string match. Three long-span types are unscoreable under this metric across all combos.
- Wall-clock and dollar cost not measured per combo. Mistral run took ~3.5 hours (Large-tier rate limits dominated).
- GEPA's internal per-doc-then-avg val metric was very harsh on Mistral (val scores ~0.02 for Small tasks) because Mistral over-extracts spurious entity types. The corpus-aggregate test metric is what's reported above and showed optimization helped despite the noisy val signal.
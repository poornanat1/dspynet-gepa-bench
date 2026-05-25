# dspynet-gepa-bench

[GEPA](https://github.com/poornanat1/DSpyNet/tree/fix/gepa-parity) (from [DSpyNet](https://github.com/al322se/DSpyNet), open as [PR #2](https://github.com/al322se/DSpyNet/pull/2)) on SEC S-1 key-information extraction from [RealKIE](https://arxiv.org/abs/2403.20101). Six (task LM, reflection LM) combinations per provider; Anthropic and Mistral built in.

## Headline (Anthropic)

| Task LM | Reflect LM | Baseline | GEPA | Δ (pp) |
|---|---|---:|---:|---:|
| Haiku 4.5 | Haiku 4.5 | 0.386 | 0.616 | +23.0 |
| Haiku 4.5 | Sonnet 4.6 | 0.386 | 0.622 | +23.6 |
| Haiku 4.5 | Opus 4.7 | 0.386 | 0.622 | +23.6 |
| Sonnet 4.6 | Sonnet 4.6 | 0.513 | 0.574 | +6.1 |
| Sonnet 4.6 | Opus 4.7 | 0.513 | 0.555 | +4.2 |
| Opus 4.7 | Opus 4.7 | 0.641 | 0.703 | +6.2 |

Mistral matrix in flight; see [`docs/results.md`](docs/results.md).

## Docs

- [Purpose](docs/purpose.md) — what we're measuring and why
- [Setup](docs/setup.md) — dataset, task, metric, GEPA config, providers
- [Results](docs/results.md) — matrix + observations + caveats

Per-entity-type tables and the GEPA-written instructions: [`results/benchmark-<provider>.md`](results/).

## Running

```
# .env
ANTHROPIC_API_KEY=sk-ant-...        # for the Anthropic matrix
MISTRAL_API_KEY=...                  # for the Mistral matrix

# data
aws s3 cp s3://project-fruitfly/s1_truncated/train.csv data/s1_train.csv \
  --endpoint-url=https://s3.us-east-2.wasabisys.com --no-sign-request
aws s3 cp s3://project-fruitfly/s1_truncated/test.csv  data/s1_test.csv \
  --endpoint-url=https://s3.us-east-2.wasabisys.com --no-sign-request

# matrix (auto-picks provider from env, or use --provider=)
dotnet run                              # full 6-combo matrix
dotnet run -- --provider=mistral        # explicit provider
dotnet run -- --smoke                   # 1 combo, 10 train / 5 test
```
# Purpose

[DSpyNet](https://github.com/al322se/DSpyNet) has a GEPA implementation (Genetic-Pareto reflective prompt evolution; see [`fix/gepa-parity`](https://github.com/poornanat1/DSpyNet/tree/fix/gepa-parity), open as [PR #2](https://github.com/al322se/DSpyNet/pull/2)). This repo runs it on a non-toy extraction task to measure:

- whether the optimizer improves a baseline prompt on a real dataset
- how the choice of task LM vs reflection LM moves the result
- whether a cheap task LM with a strong reflection LM is competitive with an all-strong setup

One dataset, six combinations, three tiers per LM provider — Anthropic (Haiku → Sonnet → Opus) and Mistral (Small → Medium → Large) built in. See [setup](setup.md) for provider details.

## Why this task

The dataset comes from [RealKIE (Townsend et al., 2024)](https://arxiv.org/abs/2403.20101), which enumerates what makes enterprise key-information extraction hard: "*sparse annotations within long documents that cause class imbalance issues*", "*complex tabular layout that must be considered to discriminate between similar labels*", and entity values that range "*from simple dates and prices to long-form clauses*". The paper evaluates fine-tuned encoder models (DeBERTa-v3, Longformer, LayoutLM-v3); it does not study LLM prompting.

Those same difficulties — disambiguating Agent Name from Company Name on a cover page, knowing whether to strip a trailing period from an entity, knowing not to split a multi-line address — are exactly the kind of unwritten annotation conventions a static prompt cannot encode without examples. GEPA's reflection LM reads the gold spans and writes those conventions into the instruction. The benchmark here measures how much that helps in practice, on one of the paper's datasets.
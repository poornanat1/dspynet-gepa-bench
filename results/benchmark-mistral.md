# RealKIE SEC S-1 — GEPA Model Combo Benchmark

_Generated 2026-05-26 04:24 UTC_

> **LM config:** provider defaults — no `temperature` or `reasoning_effort` overrides. For Mistral that means default temperature (~0.7) and reasoning on by default for hybrid tiers (Small 4, Medium 3.5).

## Setup

- Dataset: RealKIE `s1_truncated`, [Indico](https://indicodatasolutions.github.io/RealKIE/)
- Train passages: **75** | Test passages: **50**
- Entity types: **24**
- Metric: per-type micro-F1, then macro-averaged across types
- GEPA budget: `Auto = Light`, reflection minibatch = 3, val split = 0.3, seed = 42

## Macro F1 Summary

| Task LM | Reflection LM | Baseline | GEPA | Δ (pp) | Notes |
|---|---|---:|---:|---:|---|
| Mistral Small 4 | Mistral Small 4 | 0.358 | 0.370 | +1.2 |  |
| Mistral Small 4 | Mistral Medium 3.5 | 0.358 | 0.528 | +17.0 |  |
| Mistral Small 4 | Mistral Large (2512) | 0.358 | 0.458 | +10.0 |  |
| Mistral Medium 3.5 | Mistral Medium 3.5 | 0.370 | 0.461 | +9.1 |  |
| Mistral Medium 3.5 | Mistral Large (2512) | 0.370 | 0.468 | +9.8 |  |
| Mistral Large (2512) | Mistral Large (2512) | 0.396 | 0.538 | +14.2 |  |

## Task LM = Mistral Large (2512)

Baseline macro F1: **0.396**

Per-entity-type F1 (and Δ vs baseline in pp):

| Entity type | Baseline | reflect=`Mistral Large (2512)` |
|---|---:|---:|
| (Header) Description of Securities | — | 0.000 (—) |
| (Header) Dividend Policy | 1.000 | 1.000 (0.0) |
| (Header) Prospectus Summary | 0.667 | 1.000 (+33.3) |
| (Header) Risks To The Business | 0.000 | 0.000 (0.0) |
| Agent Address | 0.000 | 1.000 (+100.0) |
| Agent Name | 0.364 | 0.800 (+43.6) |
| Agent Telephone | 0.667 | 1.000 (+33.3) |
| Amount Registered | 0.296 | 0.444 (+14.8) |
| Attorney Names | 0.308 | 0.308 (0.0) |
| Company Address | 0.400 | 1.000 (+60.0) |
| Company Name | 0.182 | 0.286 (+10.4) |
| Company Officer | 0.815 | 0.815 (0.0) |
| Company Officer Title | 0.348 | 0.737 (+38.9) |
| Date of Prospectus | 0.667 | 0.800 (+13.3) |
| Description of Securities (1st Para) | 0.000 | — (—) |
| Dividend Policy (1st Para) | 0.000 | 0.000 (0.0) |
| EIN | 1.000 | 1.000 (0.0) |
| Joint Book Runners | 0.769 | 0.533 (-23.6) |
| Law Firm Address | 0.308 | 0.308 (0.0) |
| Law Firm Name | 1.000 | 1.000 (0.0) |
| Max Price | 0.154 | 0.250 (+9.6) |
| Prospectus Summary (1st Para) | 0.000 | 0.000 (0.0) |
| Risk Clauses | 0.021 | 0.027 (+0.6) |
| Title of Security Registered | 0.148 | 0.071 (-7.7) |

## Task LM = Mistral Medium 3.5

Baseline macro F1: **0.370**

Per-entity-type F1 (and Δ vs baseline in pp):

| Entity type | Baseline | reflect=`Mistral Large (2512)` | reflect=`Mistral Medium 3.5` |
|---|---:|---:|---:|
| (Header) Description of Securities | — | — (—) | — (—) |
| (Header) Dividend Policy | 1.000 | 1.000 (0.0) | 1.000 (0.0) |
| (Header) Prospectus Summary | 0.667 | 1.000 (+33.3) | 0.667 (0.0) |
| (Header) Risks To The Business | 0.000 | 0.667 (+66.7) | 0.000 (0.0) |
| Agent Address | 0.000 | 0.000 (0.0) | 0.000 (0.0) |
| Agent Name | 1.000 | 1.000 (0.0) | 1.000 (0.0) |
| Agent Telephone | 1.000 | 1.000 (0.0) | 1.000 (0.0) |
| Amount Registered | 0.333 | 0.167 (-16.7) | 0.727 (+39.4) |
| Attorney Names | 0.308 | 0.308 (0.0) | 0.296 (-1.1) |
| Company Address | 0.000 | 0.000 (0.0) | 0.000 (0.0) |
| Company Name | 0.061 | 0.286 (+22.5) | 0.250 (+18.9) |
| Company Officer | 0.759 | 0.815 (+5.6) | 0.526 (-23.2) |
| Company Officer Title | 0.583 | 0.824 (+24.0) | 0.714 (+13.1) |
| Date of Prospectus | 0.000 | 0.000 (0.0) | 0.800 (+80.0) |
| Description of Securities (1st Para) | 0.000 | — (—) | — (—) |
| Dividend Policy (1st Para) | 0.000 | 0.000 (0.0) | 0.000 (0.0) |
| EIN | 1.000 | 1.000 (0.0) | 1.000 (0.0) |
| Joint Book Runners | 0.667 | 0.667 (0.0) | 0.714 (+4.8) |
| Law Firm Address | 0.000 | 0.000 (0.0) | 0.000 (0.0) |
| Law Firm Name | 0.909 | 1.000 (+9.1) | 1.000 (+9.1) |
| Max Price | 0.222 | 0.286 (+6.3) | 0.286 (+6.3) |
| Prospectus Summary (1st Para) | 0.000 | 0.000 (0.0) | 0.000 (0.0) |
| Risk Clauses | 0.006 | 0.000 (-0.6) | 0.000 (-0.6) |
| Title of Security Registered | 0.000 | 0.286 (+28.6) | 0.167 (+16.7) |

## Task LM = Mistral Small 4

Baseline macro F1: **0.358**

Per-entity-type F1 (and Δ vs baseline in pp):

| Entity type | Baseline | reflect=`Mistral Large (2512)` | reflect=`Mistral Medium 3.5` | reflect=`Mistral Small 4` |
|---|---:|---:|---:|---:|
| (Header) Description of Securities | 0.000 | 0.000 (0.0) | 0.000 (0.0) | 0.000 (0.0) |
| (Header) Dividend Policy | 1.000 | 1.000 (0.0) | 1.000 (0.0) | 1.000 (0.0) |
| (Header) Prospectus Summary | 0.333 | 0.500 (+16.7) | 0.667 (+33.3) | 0.500 (+16.7) |
| (Header) Risks To The Business | 0.000 | 0.011 (+1.1) | 0.029 (+2.9) | 0.000 (0.0) |
| Agent Address | 0.000 | 1.000 (+100.0) | 1.000 (+100.0) | 0.000 (0.0) |
| Agent Name | 0.400 | 0.364 (-3.6) | 1.000 (+60.0) | 0.364 (-3.6) |
| Agent Telephone | 1.000 | 1.000 (0.0) | 1.000 (0.0) | 1.000 (0.0) |
| Amount Registered | 0.417 | 0.333 (-8.3) | 0.364 (-5.3) | 0.370 (-4.6) |
| Attorney Names | 0.167 | 0.308 (+14.1) | 0.167 (0.0) | 0.308 (+14.1) |
| Company Address | 0.000 | 0.667 (+66.7) | 1.000 (+100.0) | 0.000 (0.0) |
| Company Name | 0.062 | 0.133 (+7.1) | 0.333 (+27.1) | 0.042 (-2.1) |
| Company Officer | 0.710 | 0.611 (-9.9) | 0.846 (+13.6) | 0.687 (-2.2) |
| Company Officer Title | 0.824 | 0.824 (0.0) | 0.875 (+5.1) | 0.636 (-18.7) |
| Date of Prospectus | 0.571 | 0.444 (-12.7) | 1.000 (+42.9) | 1.000 (+42.9) |
| Description of Securities (1st Para) | 0.000 | 0.000 (0.0) | 0.000 (0.0) | 0.000 (0.0) |
| Dividend Policy (1st Para) | 0.000 | 0.000 (0.0) | 0.000 (0.0) | 0.000 (0.0) |
| EIN | 1.000 | 1.000 (0.0) | 1.000 (0.0) | 1.000 (0.0) |
| Joint Book Runners | 0.625 | 0.706 (+8.1) | 0.706 (+8.1) | 0.706 (+8.1) |
| Law Firm Address | 0.000 | 0.333 (+33.3) | 0.167 (+16.7) | 0.000 (0.0) |
| Law Firm Name | 1.000 | 1.000 (0.0) | 0.889 (-11.1) | 1.000 (0.0) |
| Max Price | 0.250 | 0.250 (0.0) | 0.400 (+15.0) | 0.250 (0.0) |
| Prospectus Summary (1st Para) | 0.000 | 0.000 (0.0) | 0.000 (0.0) | 0.000 (0.0) |
| Risk Clauses | 0.007 | 0.013 (+0.6) | 0.000 (-0.7) | 0.009 (+0.2) |
| Title of Security Registered | 0.222 | 0.500 (+27.8) | 0.222 (0.0) | 0.000 (-22.2) |

## Final Instructions Produced by GEPA

### task=Mistral Small 4 · reflect=Mistral Small 4

```text
Extract key information from the SEC S-1 filing passage. For each entity type listed, find every span in the passage whose text fits the type and report it. Return a JSON object whose keys are the entity type names and whose values are arrays of the exact extracted strings. If no spans of a type are present, return an empty array for that key. Output only the JSON object — no commentary, no markdown fences.
```

### task=Mistral Small 4 · reflect=Mistral Medium 3.5

```text
{
  "task": "Extract key information from the SEC S-1 filing passage. For each entity type listed, find every span in the passage whose text fits the type and report it.",
  "instructions": [
    "Return a JSON object whose keys are the entity type names and whose values are arrays of the exact extracted strings.",
    "If no spans of a type are present, return an empty array for that key.",
    "Output only the JSON object — no commentary, no markdown fences.",
    "Ensure that the extracted spans match the exact text in the passage, including punctuation and line breaks.",
    "For 'Agent Address', 'Agent Name', and 'Agent Telephone', extract the information related to the agent for service as specified in the passage.",
    "For 'Attorney Names', extract the full names of the attorneys, including their titles (e.g., 'Esq.').",
    "For 'Law Firm Name' and 'Law Firm Address', extract the full names and addresses of the law firms mentioned.",
    "For 'Risk Clauses', extract only the complete sentences that describe risks to the business, ensuring they are not partial or incomplete.",
    "For 'Amount Registered', extract only the exact amounts of securities being registered, not descriptions or additional context.",
    "For 'Description of Securities (1st Para)', extract only the first paragraph that describes the securities, not individual phrases or partial sentences.",
    "For 'Joint Book Runners', extract only the names of the joint book runners explicitly mentioned as such in the passage.",
    "Do not include any additional context or partial sentences in the extracted spans unless they are explicitly part of the entity type's definition."
  ],
  "entity_types": [
    "(Header) Description of Securities",
    "(Header) Dividend Policy",
    "(Header) Prospectus Summary",
    "(Header) Risks To The Business",
    "Agent Address",
    "Agent Name",
    "Agent Telephone",
    "Amount Registered",
    "Attorney Names",
    "Company Address",
    "Company Name",
    "Company Officer",
    "Company Officer Title",
    "Date of Prospectus",
    "Description of Securities (1st Para)",
    "Dividend Policy (1st Para)",
    "EIN",
    "Joint Book Runners",
    "Law Firm Address",
    "Law Firm Name",
    "Max Price",
    "Prospectus Summary (1st Para)",
    "Risk Clauses",
    "Title of Security Registered"
  ]
}
```

### task=Mistral Small 4 · reflect=Mistral Large (2512)

```text
Extract key information from the SEC S-1 filing passage. For each entity type listed, find every span in the passage whose text fits the type and report it. Return a JSON object whose keys are the entity type names and whose values are arrays of the exact extracted strings. If no spans of a type are present, return an empty array for that key. Output only the JSON object — no commentary, no markdown fences.
```

### task=Mistral Medium 3.5 · reflect=Mistral Medium 3.5

```text
{
  "instruction": "Extract key information from the SEC S-1 filing passage. For each entity type listed in the input, find every span in the passage whose text fits the type and report it. Return a JSON object whose keys are the exact entity type names (including parentheses, e.g., '(Header) Description of Securities') and whose values are arrays of the exact extracted strings. If no spans of a type are present, return an empty array for that key. Output only the JSON object — no commentary, no markdown fences.

### Strict Guidelines:
1. **Exact Matching**: Only extract spans that exactly match the entity type's description. Do not infer, paraphrase, or generalize.
2. **Headers**: Extract only exact header text as it appears in the passage (e.g., 'DESCRIPTION OF SECURITIES'). Do not include subheaders unless explicitly listed as entity types.
3. **Risk Clauses**: Extract only full sentences or clauses that explicitly describe risks, uncertainties, or potential negative outcomes. A risk clause must:
   - Be a complete, standalone sentence or clause (no partial sentences or fragments).
   - Directly describe a risk, uncertainty, or negative outcome (e.g., 'If we fail to consummate a business combination, we will liquidate.').
   - Not include contextual, introductory, or explanatory sentences that do not directly describe a risk.
   - Not include duplicates or near-duplicates of the same risk.
   - Start with a capital letter and end with proper punctuation (e.g., periods, exclamation marks).
   - Avoid sentences that are purely hypothetical or speculative without clear risk implications.
4. **Agent Name**: Only extract names explicitly identified as agents (e.g., underwriters, warrant agents, or representatives) in the passage. Do not assume entities like 'EarlyBirdCapital, Inc.' or 'Continental Stock Transfer & Trust Company' are agents unless the passage explicitly states their role as such. If the passage lists an entity as the 'agent for service', extract that entity as the Agent Name.
5. **Agent Address/Telephone**: Only extract the address and telephone number if they are explicitly tied to the agent for service in the passage. Do not extract company addresses or telephone numbers unless they are explicitly labeled as the agent's.
6. **Title of Security Registered**: Extract only formal titles of securities explicitly mentioned as registered (e.g., 'Common Stock', 'Class A common stock'). Avoid generic terms (e.g., 'common stock', 'preferred stock') unless they are explicitly labeled as registered titles in the passage (e.g., 'shares of common stock' does not count; 'Class A common stock' does).
7. **Max Price**: Extract only explicit maximum price values tied to securities (e.g., '$10.00 per share') if they are clearly stated as such in the passage. Do not extract prices mentioned in other contexts (e.g., hypothetical scenarios, trust account thresholds, or warrant exercise prices).
8. **Description of Securities (1st Para)**: Extract only the first paragraph under the 'DESCRIPTION OF SECURITIES' header, if present. Do not extract subsequent paragraphs or unrelated text.
9. **Company Officer/Title**: Extract full names and titles exactly as they appear, including punctuation, hyphens, and capitalization. Do not truncate or modify. Only extract if the passage explicitly lists the person as an officer (e.g., 'Co-Chief Executive Officer').
10. **Attorney Names/Law Firm Name/Address**: Extract only if the passage explicitly lists attorneys or law firms in the context of legal representation. Combine multi-part law firm names (e.g., 'Graubard Miller' and 'Greenberg Traurig, LLP' should be merged as 'Graubard Miller Greenberg Traurig, LLP' if they appear together). Extract addresses exactly as they appear, including line breaks and punctuation.
11. **EIN**: Extract only the Employer Identification Number if explicitly labeled as such (e.g., 'I.R.S. Employer Identification Number').
12. **Date of Prospectus**: Extract only the exact date mentioned in the context of the prospectus filing (e.g., 'As filed with the Securities and Exchange Commission on January 11, 2021' → 'January 11, 2021').
13. **Empty Arrays**: If an entity type is not present in the passage, return an empty array for that key. Do not omit the key.
14. **No Additional Keys**: Do not include keys not listed in the input's `EntityTypes`. Stick strictly to the provided types.

### Domain-Specific Notes:
- **SEC S-1 Context**: The passage is from an SEC S-1 filing, which includes sections like 'DESCRIPTION OF SECURITIES', 'Risk Factors', 'Prospectus Summary', and details about securities, agents, and corporate governance.
- **Trust Account References**: Phrases like '$10.00 per share' in the context of trust accounts or liquidation are not 'Max Price' unless explicitly tied to a security's offering price.
- **Agent Roles**: The 'agent for service' is typically listed in the S-1 filing under a dedicated section. Extract this entity as the Agent Name, and its associated address/telephone as Agent Address/Agent Telephone.
- **Security Titles**: Only extract titles if they are explicitly mentioned as registered securities (e.g., 'Class A common stock', 'Common Stock'). Avoid informal references like 'shares of common stock'.
- **Risk Clauses**: Focus on sentences that start with risk-indicating phrases like 'If we...', 'We may...', 'This could...', 'Our inability to...', etc. Avoid sentences that are purely explanatory or contextual.

### Example Clarifications:
- For `(Header) Description of Securities`, extract only the exact string 'DESCRIPTION OF SECURITIES' if it appears as a header.
- For `Risk Clauses`, extract only standalone risk descriptions (e.g., 'If we fail to consummate a business combination, we will liquidate.'). Do not extract:
  - Sentences like 'Furthermore, some of these risks may be outside of our control...' (too vague).
  - Partial sentences or fragments.
  - Non-risk sentences (e.g., 'We could seek to deviate from the acquisition criteria...' is not a risk unless it describes a negative outcome).
- For `Agent Name`, extract only if the passage explicitly states the role (e.g., '(Name, address, including zip code, and telephone number, including area code, of agent for service)' → extract the name listed there).
- For `Title of Security Registered`, extract only if the passage explicitly mentions the title in the context of registration (e.g., 'shares of Class A common stock' → 'Class A common stock').

### Output Format:
{
  \"(Header) Description of Securities\": [\"DESCRIPTION OF SECURITIES\"],
  \"Risk Clauses\": [\"Full risk sentence 1\", \"Full risk sentence 2\"],
  \"Agent Name\": [\"Agent for Service Name\"],
  ...
}"
}
```

### task=Mistral Medium 3.5 · reflect=Mistral Large (2512)

```text
{
  "instruction": "Extract key information from SEC S-1 filing passages according to the following entity types and rules. Return a JSON object where each key is an entity type name and the value is an array of exact text spans extracted from the passage that match the entity type. If no spans match a type, return an empty array for that key. Output only the JSON object—no commentary, no markdown fences, no deviations from the specified format.\n\n### Entity Types and Extraction Rules:\n\n1. **(Header) [Type]**: Extract only the exact header text (e.g., \"RISK FACTORS\") that matches the specified type (e.g., \"(Header) Risks To The Business\"). Do not include subheaders or body text under the header.\n   - Example: For \"(Header) Risks To The Business\", extract \"RISK FACTORS\" but not \"Risks Relating to our Search...\".\n\n2. **Risk Clauses**: Extract full sentences or coherent multi-sentence spans that describe specific risks to the business, investors, or operations. Follow these guidelines:\n   - Include only complete clauses that explicitly state a risk (e.g., \"Our public stockholders may not be afforded an opportunity to vote...\").\n   - Exclude generic warnings (e.g., \"An investment in our securities involves a high degree of risk.\") unless they are part of a larger risk clause.\n   - Exclude procedural or explanatory text that does not describe a risk (e.g., \"For example, Nasdaq rules currently allow us...\").\n   - Preserve line breaks and formatting as they appear in the original text.\n\n3. **Joint Book Runners**: Extract the exact name of the underwriting firm(s) listed as the sole or joint book-running manager(s).\n   - Example: \"EarlyBirdCapital, Inc.\"\n\n4. **Amount Registered**: Extract only numerical values representing the total offering amount (e.g., \"$50,000,000\") or the maximum offering amount if an over-allotment option is mentioned (e.g., \"$57,500,000\"). Do not include per-unit amounts or other financial figures.\n\n5. **Company Name**: Extract the exact legal name of the company as it appears in the filing (e.g., \"IGNYTE ACQUISITION CORP.\").\n\n6. **Max Price**: Extract the per-unit offering price (e.g., \"$10.00\").\n\n7. **Title of Security Registered**: Extract the type of security being registered (e.g., \"units\", \"shares of common stock\").\n\n8. **Other Entity Types**: For all other entity types (e.g., Agent Address, Attorney Names, Company Officer), extract only if the passage explicitly contains the requested information. Do not infer or include partial matches.\n\n### Domain-Specific Rules:\n- **SEC S-1 Context**: Focus on passages that discuss risks, financial terms, or corporate governance (e.g., stockholder votes, business combinations, redemption rights). Ignore table of contents, boilerplate disclaimers, or forward-looking statements unless they contain risk clauses.\n- **Risk Clauses**: Prioritize clauses that:\n  - Describe legal, operational, or financial risks (e.g., inability to consummate a business combination, stockholder voting limitations).\n  - Mention specific thresholds (e.g., \"20% of outstanding shares\"), regulatory bodies (e.g., \"Nasdaq\"), or contractual conditions (e.g., \"minimum net worth\").\n- **Headers**: Only extract headers that exactly match the entity type (e.g., \"RISK FACTORS\" for \"(Header) Risks To The Business\").\n\n### Strategy:\n1. **Header Extraction**: Scan the passage for all-caps or title-case headers. Match the header text to the entity type (e.g., \"RISK FACTORS\" → \"(Header) Risks To The Business\").\n2. **Risk Clauses**: Identify spans that:\n   - Begin with a risk indicator (e.g., \"We may not\", \"Our ability to\", \"If [X] occurs\").\n   - End at the conclusion of a complete thought (e.g., before a new risk or procedural statement).\n   - Exclude introductory or transitional phrases (e.g., \"You should consider...\").\n3. **Financial/Structural Entities**: Extract exact numerical values or names (e.g., \"$10.00\", \"EarlyBirdCapital, Inc.\") without additional context.\n4. **Validation**: Ensure extracted spans are:\n   - Verbose for risk clauses (include full context).\n   - Minimal for headers/names (exact matches only).\n   - Free of spurious additions (e.g., do not include \"See 'Risk Factors'\" as a risk clause).\n\n### Example Output Structure:\n```json\n{\n  \"(Header) Risks To The Business\": [\"RISK FACTORS\"],\n  \"Risk Clauses\": [\n    \"Our public stockholders may not be afforded an opportunity to vote on our proposed business combination, unless such vote is required by law or Nasdaq, which means we may consummate our initial business combination even though a majority of our public stockholders do not support such a combination.\",\n    \"If we seek stockholder approval of our initial business combination, our sponsor, directors and officers have agreed to vote in favor of such initial business combination, regardless of how our public stockholders vote.\"\n  ],\n  \"Joint Book Runners\": [\"EarlyBirdCapital, Inc.\"],\n  \"Amount Registered\": [\"$50,000,000\", \"$57,500,000\"],\n  \"Company Name\": [\"IGNYTE ACQUISITION CORP.\"],\n  \"Max Price\": [\"$10.00\"],\n  \"Title of Security Registered\": [\"units\"],\n  \"Agent Address\": [],\n  ...\n}\n```"
}
```

### task=Mistral Large (2512) · reflect=Mistral Large (2512)

```text
{
  "instruction": "Extract key information from the SEC S-1 filing passage according to the specified entity types. Your task is to identify and extract **only the exact spans of text** that directly correspond to each entity type, ensuring precision and relevance. Return a JSON object where each key is an entity type and the value is an array of exact extracted strings. If no spans of a type are present, return an empty array for that key. Output only the JSON object—no commentary, no markdown fences, and no extraneous text.\n\n### Key Guidelines:\n1. **Risk Clauses**: Extract **only the standalone risk statements** explicitly labeled as risks in the passage. These are typically concise, self-contained sentences or clauses that describe a specific risk (e.g., liability, compliance failure, operational disruption). Do **not** include:\n   - Supporting details, legal explanations, or procedural descriptions (e.g., references to DGCL sections, Sarbanes-Oxley compliance steps).\n   - Hypothetical scenarios or conditional statements unless they are explicitly framed as risks.\n   - Duplicate or near-duplicate spans (e.g., variations of the same risk phrased differently).\n\n2. **Agent Name/Address/Telephone**: Extract **only the exact name, address, or telephone number** of the transfer agent or warrant agent mentioned in the passage. Ignore generic references (e.g., \"warrant agent\") or partial details.\n\n3. **Max Price**: Extract **only the explicit numerical values** tied to warrant redemption prices, exercise prices, or other financial thresholds (e.g., \"$18.00\", \"$0.01\"). Do not infer or extrapolate prices from contextual descriptions.\n\n4. **Title of Security Registered**: Extract **only the exact names of securities** (e.g., \"common stock\", \"warrants\") explicitly mentioned as registered or issuable. Do not include generic terms (e.g., \"shares\") unless they are part of a formal title.\n\n5. **Headers and Paragraphs**: For entity types like `(Header) Description of Securities` or `Description of Securities (1st Para)`, extract **only the exact header text** (e.g., \"Description of Securities\") or the **first paragraph** under that header if explicitly requested. Do not include headers or paragraphs that are not directly labeled in the passage.\n\n6. **General Strategy**:\n   - **Precision Over Recall**: Prioritize exact matches to the entity type’s definition. If a span is ambiguous or partially relevant, exclude it.\n   - **Avoid Over-Extraction**: Do not include spans that are tangentially related to the entity type (e.g., legal context for a risk clause).\n   - **No Aggregation**: Each extracted span must be a direct quote from the passage; do not paraphrase or combine text.\n\n### Domain-Specific Rules:\n- **SEC S-1 Filings**: Passages often describe risks in dense legal language. Focus on **standalone risk statements** (e.g., \"Our stockholders may be held liable for claims by third parties...\") and ignore procedural or explanatory text.\n- **Warrant Agreements**: Extract agent details (name/address/telephone) only if explicitly stated (e.g., \"Continental Stock Transfer & Trust Company, as warrant agent\").\n- **Financial Thresholds**: For `Max Price`, extract only the exact dollar amounts tied to redemption/exercise conditions (e.g., \"$18.00 per share\").\n\n### Example Output Structure:\n```json\n{\n  \"Risk Clauses\": [\"Exact risk statement 1\", \"Exact risk statement 2\"],\n  \"Agent Name\": [\"Continental Stock Transfer & Trust Company\"],\n  \"Max Price\": [\"$18.00\", \"$0.01\"],\n  \"Title of Security Registered\": [\"common stock\", \"warrants\"],\n  \"(Header) Description of Securities\": [],\n  ...\n}\n```"
}
```


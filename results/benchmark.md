# RealKIE SEC S-1 — GEPA Model Combo Benchmark

_Generated 2026-05-24 04:50 UTC_

## Setup

- Dataset: RealKIE `s1_truncated`, [Indico](https://indicodatasolutions.github.io/RealKIE/)
- Train passages: **75** | Test passages: **50**
- Entity types: **24**
- Metric: per-type micro-F1, then macro-averaged across types
- GEPA budget: `Auto = Light`, reflection minibatch = 3, val split = 0.3, seed = 42

## Macro F1 Summary

| Task LM | Reflection LM | Baseline | GEPA | Δ (pp) | Notes |
|---|---|---:|---:|---:|---|
| Haiku 4.5 | Haiku 4.5 | 0.386 | 0.616 | +23.0 |  |
| Haiku 4.5 | Sonnet 4.6 | 0.386 | 0.622 | +23.6 |  |
| Haiku 4.5 | Opus 4.7 | 0.386 | 0.622 | +23.6 |  |
| Sonnet 4.6 | Sonnet 4.6 | 0.513 | 0.574 | +6.1 |  |
| Sonnet 4.6 | Opus 4.7 | 0.513 | 0.555 | +4.2 |  |
| Opus 4.7 | Opus 4.7 | 0.641 | 0.703 | +6.2 |  |

## Task LM = Haiku 4.5

Baseline macro F1: **0.386**

Per-entity-type F1 (and Δ vs baseline in pp):

| Entity type | Baseline | reflect=`Haiku 4.5` | reflect=`Opus 4.7` | reflect=`Sonnet 4.6` |
|---|---:|---:|---:|---:|
| (Header) Description of Securities | — | — (—) | — (—) | — (—) |
| (Header) Dividend Policy | 0.000 | 1.000 (+100.0) | 1.000 (+100.0) | 1.000 (+100.0) |
| (Header) Prospectus Summary | 0.000 | 0.667 (+66.7) | 1.000 (+100.0) | 0.667 (+66.7) |
| (Header) Risks To The Business | 0.000 | 1.000 (+100.0) | 0.667 (+66.7) | 1.000 (+100.0) |
| Agent Address | 0.500 | 1.000 (+50.0) | 1.000 (+50.0) | 1.000 (+50.0) |
| Agent Name | 0.444 | 1.000 (+55.6) | 1.000 (+55.6) | 1.000 (+55.6) |
| Agent Telephone | 1.000 | 1.000 (0.0) | 1.000 (0.0) | 1.000 (0.0) |
| Amount Registered | 0.375 | 0.235 (-14.0) | 0.444 (+6.9) | 0.444 (+6.9) |
| Attorney Names | 0.308 | 0.308 (0.0) | 0.308 (0.0) | 0.308 (0.0) |
| Company Address | 0.286 | 1.000 (+71.4) | 1.000 (+71.4) | 1.000 (+71.4) |
| Company Name | 0.091 | 0.333 (+24.2) | 0.333 (+24.2) | 0.333 (+24.2) |
| Company Officer | 0.815 | 0.815 (0.0) | 0.815 (0.0) | 0.846 (+3.1) |
| Company Officer Title | 0.824 | 0.824 (0.0) | 0.824 (0.0) | 0.875 (+5.1) |
| Date of Prospectus | 0.500 | 1.000 (+50.0) | 1.000 (+50.0) | 1.000 (+50.0) |
| Description of Securities (1st Para) | — | — (—) | — (—) | — (—) |
| Dividend Policy (1st Para) | 0.000 | 0.000 (0.0) | 0.000 (0.0) | 0.000 (0.0) |
| EIN | 1.000 | 1.000 (0.0) | 1.000 (0.0) | 1.000 (0.0) |
| Joint Book Runners | 0.545 | 0.000 (-54.5) | 0.000 (-54.5) | 0.000 (-54.5) |
| Law Firm Address | 0.000 | 0.308 (+30.8) | 0.308 (+30.8) | 0.308 (+30.8) |
| Law Firm Name | 1.000 | 1.000 (0.0) | 1.000 (0.0) | 1.000 (0.0) |
| Max Price | 0.333 | 0.333 (0.0) | 0.286 (-4.8) | 0.400 (+6.7) |
| Prospectus Summary (1st Para) | 0.000 | 0.000 (0.0) | 0.000 (0.0) | 0.000 (0.0) |
| Risk Clauses | 0.000 | 0.000 (0.0) | 0.493 (+49.3) | 0.000 (0.0) |
| Title of Security Registered | 0.471 | 0.727 (+25.7) | 0.200 (-27.1) | 0.500 (+2.9) |

## Task LM = Opus 4.7

Baseline macro F1: **0.641**

Per-entity-type F1 (and Δ vs baseline in pp):

| Entity type | Baseline | reflect=`Opus 4.7` |
|---|---:|---:|
| (Header) Description of Securities | — | — (—) |
| (Header) Dividend Policy | 1.000 | 1.000 (0.0) |
| (Header) Prospectus Summary | 1.000 | 0.500 (-50.0) |
| (Header) Risks To The Business | 0.143 | 0.000 (-14.3) |
| Agent Address | 1.000 | 1.000 (0.0) |
| Agent Name | 1.000 | 1.000 (0.0) |
| Agent Telephone | 1.000 | 1.000 (0.0) |
| Amount Registered | 0.727 | 0.500 (-22.7) |
| Attorney Names | 0.308 | 0.308 (0.0) |
| Company Address | 1.000 | 1.000 (0.0) |
| Company Name | 0.222 | 0.333 (+11.1) |
| Company Officer | 0.815 | 0.815 (0.0) |
| Company Officer Title | 0.824 | 0.824 (0.0) |
| Date of Prospectus | 1.000 | 1.000 (0.0) |
| Description of Securities (1st Para) | — | — (—) |
| Dividend Policy (1st Para) | 0.000 | 1.000 (+100.0) |
| EIN | 1.000 | 1.000 (0.0) |
| Joint Book Runners | 0.706 | 0.667 (-3.9) |
| Law Firm Address | 0.308 | 0.308 (0.0) |
| Law Firm Name | 1.000 | 1.000 (0.0) |
| Max Price | 0.250 | 0.800 (+55.0) |
| Prospectus Summary (1st Para) | 0.000 | 0.133 (+13.3) |
| Risk Clauses | 0.000 | 0.992 (+99.2) |
| Title of Security Registered | 0.800 | 0.286 (-51.4) |

## Task LM = Sonnet 4.6

Baseline macro F1: **0.513**

Per-entity-type F1 (and Δ vs baseline in pp):

| Entity type | Baseline | reflect=`Opus 4.7` | reflect=`Sonnet 4.6` |
|---|---:|---:|---:|
| (Header) Description of Securities | — | — (—) | — (—) |
| (Header) Dividend Policy | 0.667 | 1.000 (+33.3) | 1.000 (+33.3) |
| (Header) Prospectus Summary | 1.000 | 0.000 (-100.0) | 0.667 (-33.3) |
| (Header) Risks To The Business | 0.105 | 0.000 (-10.5) | 0.667 (+56.1) |
| Agent Address | 1.000 | 1.000 (0.0) | 1.000 (0.0) |
| Agent Name | 0.800 | 1.000 (+20.0) | 1.000 (+20.0) |
| Agent Telephone | 1.000 | 1.000 (0.0) | 1.000 (0.0) |
| Amount Registered | 0.471 | 0.444 (-2.6) | 0.444 (-2.6) |
| Attorney Names | 0.308 | 0.308 (0.0) | 0.308 (0.0) |
| Company Address | 0.800 | 1.000 (+20.0) | 1.000 (+20.0) |
| Company Name | 0.087 | 0.333 (+24.6) | 0.400 (+31.3) |
| Company Officer | 0.815 | 0.815 (0.0) | 0.526 (-28.8) |
| Company Officer Title | 0.538 | 0.824 (+28.5) | 0.714 (+17.6) |
| Date of Prospectus | 0.400 | 0.000 (-40.0) | 0.000 (-40.0) |
| Description of Securities (1st Para) | — | — (—) | — (—) |
| Dividend Policy (1st Para) | 0.000 | 0.000 (0.0) | 0.000 (0.0) |
| EIN | 1.000 | 1.000 (0.0) | 1.000 (0.0) |
| Joint Book Runners | 0.706 | 0.400 (-30.6) | 0.800 (+9.4) |
| Law Firm Address | 0.308 | 0.308 (0.0) | 0.308 (0.0) |
| Law Firm Name | 1.000 | 1.000 (0.0) | 1.000 (0.0) |
| Max Price | 0.000 | 0.667 (+66.7) | 0.800 (+80.0) |
| Prospectus Summary (1st Para) | 0.000 | 0.000 (0.0) | 0.000 (0.0) |
| Risk Clauses | 0.000 | 0.707 (+70.7) | 0.000 (0.0) |
| Title of Security Registered | 0.286 | 0.400 (+11.4) | 0.000 (-28.6) |

## Final Instructions Produced by GEPA

### task=Haiku 4.5 · reflect=Haiku 4.5

```text
Extract key information from SEC S-1 filing passages. For each entity type listed, find every span in the passage whose text matches that type and report it. Return a JSON object with entity types as keys and arrays of extracted strings as values.

CRITICAL EXTRACTION RULES:
1. **Preserve exact text**: Extract text character-for-character as it appears in the passage, including line breaks and spacing. Do NOT add punctuation, normalize line breaks to spaces, or modify whitespace.
2. **Match complete spans only**: Extract the full meaningful span for each entity. For risk clauses, extract the complete risk statement or section heading exactly as written, not partial text.
3. **Agent for service vs. Company Officer**: The "Agent Name" is the entity listed as the agent for service of process (typically the company itself or a designated agent), NOT individual officers or executives listed separately.
4. **Avoid spurious extractions**: Do not extract entities from general body text that merely mention companies, individuals, or securities. Only extract formal designated entities in their appropriate sections.
5. **Header entity types**: "(Header) Description of Securities", "(Header) Dividend Policy", "(Header) Prospectus Summary", and "(Header) Risks To The Business" refer to section headers that may appear as titles in the document. Extract these only if they appear as actual section headings/titles, not from body paragraph text.
6. **Risk Clauses specificity**: Risk clauses are typically distinct risk statements or risk section headings. Extract exactly as written in the document, preserving original formatting and line breaks. Do not add periods or other punctuation not present in the source.
7. **Empty arrays for missing types**: Return an empty array [] for any entity type not found in the passage.

ENTITY TYPE DEFINITIONS:
- Agent Address: Address of the agent for service of process
- Agent Name: Name of the agent for service of process (as listed in that section)
- Agent Telephone: Phone number of the agent for service
- Attorney Names: Individual attorney names in the format "First Last, Esq." or similar
- Company Address: Principal executive office address of the registrant
- Company Name: Official name of the registrant company
- Company Officer: Names of company officers/executives
- Company Officer Title: Titles of company officers
- Date of Prospectus: Filing date
- EIN: Employer Identification Number (I.R.S. Employer Identification Number)
- Law Firm Address: Address of law firm(s)
- Law Firm Name: Name of law firm(s)
- Risk Clauses: Risk statements or risk section headings as written in the document
- Other types: Use standard definitions for Amount Registered, Max Price, Joint Book Runners, Title of Security Registered, etc.

Output ONLY the JSON object with no additional text, markdown, or commentary.
```

### task=Haiku 4.5 · reflect=Sonnet 4.6

```text
Extract key information from the SEC S-1 filing passage. For each entity type listed, find every span in the passage whose text fits the type and report it. Return a JSON object whose keys are the EXACT entity type names as provided (preserving parentheses, spaces, capitalization, and punctuation exactly) and whose values are arrays of the exact extracted strings. If no spans of a type are present, return an empty array for that key. Output only the JSON object — no commentary, no markdown fences.

CRITICAL VERBATIM EXTRACTION RULE (applies to ALL entity types, especially Risk Clauses):
You MUST copy text EXACTLY as it appears in the passage — character by character. This means:
- PRESERVE all line breaks (newline characters \n) exactly where they appear in the source text
- PRESERVE all truncated/cut-off words exactly as they appear (e.g., "busines" stays "busines", NOT "business"; "eva" stays "eva", NOT "evaluate"; "combinat" stays "combinat", NOT "combination"; "pu" stays "pu", NOT "public")
- Do NOT add trailing periods if the source text does not have them
- Do NOT remove or replace newline characters with spaces
- Do NOT correct spelling, complete truncated words, or normalize whitespace
- The extracted string must match the source passage character-for-character

CRITICAL RULES FOR EACH ENTITY TYPE:

(Header) Description of Securities / (Header) Dividend Policy / (Header) Prospectus Summary / (Header) Risks To The Business:
These are the ALL-CAPS or bold section header titles exactly as they appear in the passage (e.g., "DESCRIPTION OF SECURITIES", "DIVIDEND POLICY", "PROSPECTUS SUMMARY", "RISK FACTORS"). Extract only the literal header text, not surrounding content.

Risk Clauses:
These are the standalone bold or heading sentences that NAME or INTRODUCE a risk — typically appearing as the title/heading of a risk factor subsection. They usually begin with "We may...", "If we...", "The determination of...", "Our...", "In connection with...", or similar phrasing and describe a risk at a high level. Do NOT extract bullet-point sub-items, elaborations, examples, or lists within the body of a risk discussion. Only extract the risk title sentence(s) that serve as the heading for each risk factor.

CRITICAL: Extract the text EXACTLY as it appears in the passage, including:
- Any mid-word line breaks (e.g., a word split across lines like "busines\ncombination" or "eva\nthe")
- Truncated words at line ends (these are OCR/PDF artifacts and must be preserved verbatim)
- Newline characters within the heading (if the heading spans multiple lines in the source)
- NO trailing period unless the period is actually present in the source text
- NO cleanup, NO word completion, NO whitespace normalization

For multi-sentence risk headings (where two sentences together form the heading, e.g., "We do not have a specified maximum conversion threshold. The absence of such a conversion threshold may make it easier for us to consummate a business combination even where a substantial number of pu\n\nstockholders seek to convert their shares to cash in connection with the vote on the business combination"), extract the full heading including both sentences and all newlines exactly as they appear.

Description of Securities (1st Para):
Extract the FULL first paragraph of the "Description of Securities" section exactly as it appears verbatim in the passage, including all line breaks and any truncated words caused by line wrapping. Do not shorten or clean up the text.

Dividend Policy (1st Para):
Extract the full first paragraph of the "Dividend Policy" section verbatim, including all line breaks and truncated words.

Prospectus Summary (1st Para):
Extract the full first paragraph of the "Prospectus Summary" section verbatim, including all line breaks and truncated words.

Agent Address / Agent Name / Agent Telephone:
These refer to the registered agent (statutory agent) of the company, not underwriters, book runners, or law firms. EarlyBirdCapital and similar entities are underwriters, not registered agents.

Title of Security Registered:
Extract only from a formal cover page or table listing the securities being registered in this specific offering. Do not extract from general body text discussions of securities.

Amount Registered, Max Price, EIN, Date of Prospectus, Company Name, Company Address, Company Officer, Company Officer Title, Attorney Names, Joint Book Runners, Law Firm Name, Law Firm Address:
Extract only clearly labeled or formally presented instances of these from the passage.

EXACT KEY NAMES: Use the entity type names exactly as provided, including any parentheses. For example: "(Header) Description of Securities", "Description of Securities (1st Para)", "Risk Clauses", etc.
```

### task=Haiku 4.5 · reflect=Opus 4.7

```text
You are an information extraction system for SEC S-1 filing passages. Given a passage and a list of entity types, extract every span from the passage that matches each entity type. Return a single JSON object whose keys are EXACTLY the entity type names as provided (including any parenthesized prefixes like "(Header)" and any suffixes like "(1st Para)"), and whose values are arrays of the exact extracted strings copied verbatim from the passage. If no spans match, use an empty array. Output ONLY the JSON object — no markdown fences, no commentary.

KEY RULES:

1. Preserve entity type keys exactly as given. Do NOT rename "(Header) Risks To The Business" to "Header Risks To The Business" — keep the parentheses and original punctuation/casing. Same for "(Header) Description of Securities", "(Header) Dividend Policy", "(Header) Prospectus Summary", "Description of Securities (1st Para)", "Dividend Policy (1st Para)", "Prospectus Summary (1st Para)".

2. **VERBATIM COPYING IS CRITICAL.** Copy text BYTE-FOR-BYTE from the passage. Specifically:
   - Preserve ALL line breaks/newlines WITHIN a span exactly as they appear in the source. Do NOT collapse a newline into a single space. If a risk-clause heading spans two lines in the passage (e.g., "...adversely affect th\nliquidity and price of our securities"), the extracted string MUST contain the literal "\n" newline character at that position.
   - Preserve ALL truncated words at line ends exactly as truncated. The passages often have OCR/text-extraction artifacts where words are cut off (e.g., "combin" instead of "combination", "less" instead of "less attractive", "financ" instead of "financial", "st" instead of "stage", "litiga" instead of "litigation", "shar\n\ncommon stock" instead of "shares of\ncommon stock", "our common\nand could entrench" instead of "our common stock\nand could entrench"). Do NOT "fix" or complete these truncations. Copy them as-is, including any blank lines in the middle.
   - Preserve curly/smart quotes (’ “ ”) exactly — do NOT convert to straight ASCII quotes (' " ").
   - Preserve spacing artifacts (e.g., "Post -Business", "non -affiliates", "Over -The -Counter").
   - Preserve hyphens and trailing markers like "(2)(4)".
   - Do NOT strip parenthetical reference markers (e.g., extract "Shares of Class A common stock included as part of the Units(2)(4)" not "Shares of Class A common stock included as part of the Units").

3. Do NOT include trailing periods at the end of a sentence-span unless the span is a clause without final punctuation in the source. For Risk Clauses, exclude the terminal period.

ENTITY TYPE DEFINITIONS AND GUIDANCE:

- (Header) Risks To The Business: The literal section header text such as "RISK FACTORS" (the all-caps header itself). Do NOT include subsection titles like "Risks Relating to our Search for..." or "General Risks".

- (Header) Description of Securities / (Header) Dividend Policy / (Header) Prospectus Summary: The literal main section header text (e.g., "DESCRIPTION OF SECURITIES", "DIVIDEND POLICY", "PROSPECTUS SUMMARY") when present.

- Risk Clauses: The bold/italicized risk-factor sub-heading sentences that introduce each individual risk. These are one-sentence (sometimes multi-line) risk titles that appear as headings IMMEDIATELY BEFORE explanatory paragraphs. They typically:
   * State a risk/concern in a single sentence (often beginning with "We", "Our", "If", "You", "Nasdaq", "Changes in", "Compliance with", "Provisions in", "Cyber", etc.).
   * Are followed by one or more explanatory body paragraphs.
   * Appear visually as a standalone heading-style line (often a line of text with blank lines around it, frequently wrapping across two lines due to formatting).
   * Often include truncated last words because of text-extraction artifacts (e.g., ending in "combin", "common", "litiga", "shar"). EXTRACT THESE TRUNCATED FORMS VERBATIM, including any blank line ("\n\n") embedded inside.
   * Do NOT extract sentences from within the explanatory body paragraphs (e.g., "The difference between the public offering price per share..." is body text, not a risk heading).
   * Do NOT extract section subtitles like "Risks Relating to...", "General Risks".
   * Extract the span WITHOUT the terminal period.
   * Preserve embedded newlines exactly as they appear in the passage — this is essential for matching the gold answer.

   Strategy for identifying Risk Clauses: scan for short, standalone heading-like sentences set off by blank lines from surrounding paragraphs. A line that starts a new logical risk topic and is followed by a paragraph elaborating on it is a Risk Clause. When such a heading wraps across multiple lines in the source, include all wrapped lines joined by the literal newline character.

- Title of Security Registered: Each row label in the Calculation of Registration Fee table describing a security class (e.g., "Units, each consisting of one share of Class A common stock, $.0001 par value, and one-fourth of one Warrant(2)(4)", "Shares of Class A common stock included as part of the Units(2)(4)", "Warrants included as part of the Units(2)(4)"). KEEP the trailing footnote markers like "(2)(4)".

- Amount Registered: The numeric "Amount to be Registered" values from the registration fee table (e.g., "25,300,000", "6,325,000"). Numbers only, no currency symbols.

- Max Price: The "Proposed Maximum Offering Price per Unit" value as the bare number without currency symbol or whitespace (e.g., "10.00"). Do NOT include aggregate offering price values.

- Agent Address, Agent Name, Agent Telephone: Information about the registered agent for service of process.
- Attorney Names: Names of attorneys listed.
- Company Address: Issuer's principal address.
- Company Name: Issuer's legal name.
- Company Officer: Names of officers.
- Company Officer Title: Titles of those officers.
- Date of Prospectus: Prospectus date.
- Description of Securities (1st Para): The first paragraph under the "Description of Securities" section.
- Dividend Policy (1st Para): The first paragraph under the "Dividend Policy" section.
- Prospectus Summary (1st Para): The first paragraph under the "Prospectus Summary" section.
- EIN: The IRS Employer Identification Number.
- Joint Book Runners: Names of joint book-running managers/underwriters.
- Law Firm Address / Law Firm Name: Counsel firm details.

For any entity type with no matching span in the passage, return an empty array. Output a single valid JSON object only, with all entity-type keys present (even if empty).
```

### task=Sonnet 4.6 · reflect=Sonnet 4.6

```text
Extract key information from the SEC S-1 filing passage. For each entity type listed, find every span in the passage whose text fits the type and report it. Return a JSON object whose keys are the entity type names and whose values are arrays of the exact extracted strings. If no spans of a type are present, return an empty array for that key. Output only the JSON object — no commentary, no markdown fences.

CRITICAL EXTRACTION RULES:

1. EXACT VERBATIM COPYING: You must copy text EXACTLY as it appears in the passage — character for character. Do NOT fix typos, add punctuation, remove line breaks, expand abbreviations, or alter the text in any way. If the passage has a truncated word (e.g., "ta\nbusiness"), copy it truncated. If the passage has a newline in the middle of a phrase, include the newline. If the passage has OCR errors or broken words, copy them as-is.

2. Risk Clauses: These are ONLY the short heading/title sentences that introduce each risk factor section — the standalone bold-style summary lines that appear before the explanatory body paragraph. They are NOT the body paragraphs themselves. Copy these heading lines exactly as they appear, including any truncation or line breaks present in the source text. Do NOT add a trailing period if the source text does not have one. Do NOT correct broken words or fix hyphenation.

3. Do NOT extract entities from incidental mentions within body text. For example:
   - Do NOT extract Agent Name just because a transfer agent is mentioned in passing within a risk paragraph.
   - Do NOT extract Max Price just because a price threshold is mentioned in a risk discussion.
   - Do NOT extract Title of Security Registered from mentions of securities within body text.
   - Only extract these entity types when they appear in their designated structured sections (cover page, registration table, header sections, etc.).

4. Entity type definitions:
   - (Header) Risks To The Business: The section header text for the risk factors section.
   - (Header) Description of Securities: The section header for description of securities.
   - (Header) Dividend Policy: The section header for dividend policy.
   - (Header) Prospectus Summary: The section header for the prospectus summary.
   - Risk Clauses: The short introductory heading sentences for individual risk factors (verbatim, as they appear).
   - Company Name, Company Address, Company Officer, Company Officer Title: Only from cover page or designated sections.
   - Agent Name, Agent Address, Agent Telephone: Only the registered agent's information from designated sections.
   - Amount Registered, Max Price, Title of Security Registered, EIN, Date of Prospectus: Only from the cover page or registration table.
   - Joint Book Runners, Law Firm Name, Law Firm Address, Attorney Names: Only from designated sections.
   - Description of Securities (1st Para), Dividend Policy (1st Para), Prospectus Summary (1st Para): Only the first paragraph of those respective named sections.
```

### task=Sonnet 4.6 · reflect=Opus 4.7

```text
You will be given a passage extracted from an SEC S-1 filing along with a list of entity type names. Your task is to extract every span in the passage that matches each entity type, returning the result as a JSON object.

OUTPUT FORMAT
- Return ONLY a single JSON object — no commentary, no markdown code fences (you may wrap in ```json fences if needed, but the JSON itself must be a single valid object).
- The JSON keys must be exactly the entity type names provided (including any prefixes like "(Header)" and any parenthetical suffixes).
- Each value must be an array of strings. If no spans of a type are present in the passage, return an empty array [] for that key.
- Every entity type provided in the input MUST appear as a key in the output JSON (with [] if no matches).

CRITICAL EXTRACTION RULES — EXACT-TEXT MATCHING (byte-for-byte)
- Extract spans EXACTLY as they appear in the passage. Do NOT normalize, clean up, or modify the text in any way.
- PRESERVE original characters EXACTLY:
  - Keep curly/smart quotes (“ ” ‘ ’) as they appear. Do NOT convert them to straight quotes (" or ').
  - Keep em dashes, en dashes, special spaces, etc., as-is.
- PRESERVE original line breaks (newline characters "\n") that occur in the middle of a span. Do NOT collapse line-wrapped text into a single line. If the passage shows a heading that breaks across two lines like "...our common\nand could entrench management", keep the "\n" exactly.
- Do NOT correct truncated/cut-off words at the end of a heading or clause (e.g., "business combin", "stockholder litiga", "our shar"). Reproduce them as-is, including any trailing blank lines / double newlines that appear before the text continues (e.g., "our shar\n\ncommon stock").
- STRIP the terminal "." (final period) from Risk Clause headings. The gold spans for "Risk Clauses" END WITHOUT the final period. Do not strip other internal punctuation.

ENTITY TYPE GUIDANCE (SEC S-1 specific)

"Risk Clauses":
- These are risk factor HEADINGS — short, sentence-style titles (typically bold/italic in the original) that immediately PRECEDE an explanatory paragraph elaborating that risk.
- They are NOT body paragraphs, NOT generic risk sentences from prose, and NOT conditional sentences embedded within paragraphs (e.g., "If some investors find our shares..." inside a paragraph is NOT a heading).
- A Risk Clause heading is usually followed by a blank line and then a paragraph that begins discussing/expanding the same topic stated in the heading.
- IMPORTANT: Not every italicized-looking sentence is a heading. A short sentence like "You may be unable to sell your securities unless a market for such securities can be established or sustained." that appears as a concluding/standalone line WITHOUT a following elaborating paragraph (i.e., the next block is another distinct heading) is typically NOT a Risk Clause heading — it is a closing remark of the prior section. Be conservative: only extract a sentence as a Risk Clause if a substantive explanatory paragraph follows it directly.
- Extract heading text verbatim, preserving newlines and truncations, and STRIP the trailing period.

"(Header) Risks To The Business":
- Only extract when the passage contains an explicit header titled "Risks To The Business" (or close variant matching that exact label).
- Do NOT extract other section labels like "General Risks" — that is a different section header and should NOT be returned under "(Header) Risks To The Business".

"(Header) Description of Securities", "(Header) Dividend Policy", "(Header) Prospectus Summary":
- Extract only when an explicit section header with that exact title is present in the passage.

"Title of Security Registered":
- Do NOT extract general mentions of securities (e.g., "common stock", "warrants") from prose.
- Only extract when the passage explicitly identifies the title of a security being registered (typically in a registration fee table or formal cover-page listing context).
- When in doubt, return [].

"Agent Name", "Agent Address", "Agent Telephone":
- Only extract when the entity is identified as the registered agent of the company.
- Continental Stock Transfer & Trust Company acting as warrant agent / transfer agent for warrants is NOT an Agent Name.

"Max Price":
- The maximum offering price per unit/share from the registration fee table or cover page.
- NOT redemption prices, exercise prices, trigger prices, or any price figures mentioned in risk factor prose.

"Company Name", "Company Address", "Company Officer", "Company Officer Title", "EIN", "Date of Prospectus", "Amount Registered", "Joint Book Runners", "Attorney Names", "Law Firm Name", "Law Firm Address":
- Only extract when explicitly present in formal cover-page / registration-table / signature-block contexts.
- Do not infer from incidental prose.

"Description of Securities (1st Para)", "Dividend Policy (1st Para)", "Prospectus Summary (1st Para)":
- Only extract if the passage clearly contains the first paragraph of that named section, appearing directly under its header.

GENERAL STRATEGY
1. Scan the passage for explicit section headers and short, standalone, sentence-style risk titles that introduce explanatory paragraphs.
2. For each candidate Risk Clause, verify a substantive elaborating paragraph follows it. If not, do not extract.
3. For each entity type, default to [] unless there is clear, in-context evidence in the passage.
4. Be conservative — prefer empty arrays over speculative extractions for unrelated mentions.
5. When extracting heading-style spans (especially Risk Clauses), reproduce text byte-for-byte from the passage, including embedded "\n" (single and double newlines), curly quotes, and word truncations, then strip ONLY the terminal period.
6. Ensure every input entity type is present as a key in the output JSON.
```

### task=Opus 4.7 · reflect=Opus 4.7

```text
You are an information extraction system for SEC S-1 prospectus filings. You will be given:
- Passage: a raw text excerpt from an S-1 filing. The text often contains PDF-extraction artifacts such as mid-word line breaks, truncated words at line ends (e.g., "commo" instead of "common", "th" instead of "that", "t" instead of "this", "dependen" instead of "dependent", "curren" instead of "currently", "an" instead of "any", "includin" instead of "including", "orde" instead of "order", "abilit" instead of "ability", "busines" instead of "business", "administrat" instead of "administrative", "compensatio" instead of "compensation", "regardle" instead of "regardless", "approximatel" instead of "approximately", "targe" instead of "target", "combin" instead of "combination", "officer" instead of "officers", "und" instead of "under", etc.), soft hyphens, and irregular whitespace.
- EntityTypes: a comma-separated list of entity type labels to extract.
- (Optionally) Extractions: a gold reference (do not rely on at inference; shown only as format guidance).

Your task: For each entity type in EntityTypes, find every span in the passage whose text fits that type and report the exact substring(s) verbatim as they appear in the passage.

Output format:
- Return ONLY a single JSON object. No commentary, no markdown fences.
- Keys = the exact entity type names from EntityTypes (preserve capitalization, parentheses, and spacing exactly; do NOT add trailing spaces or colons to keys).
- Values = arrays of strings (each string is one extracted span). Use an empty array [] if none found.

CRITICAL EXTRACTION RULES — exact-string fidelity:
1. Copy spans VERBATIM from the passage. Do NOT normalize, fix typos, rejoin hyphenated words, or merge broken lines.
   - Preserve ALL original newline characters ("\n") that appear inside the span exactly where they occur in the passage.
   - Preserve truncated words at line ends — do NOT restore the missing letters.
   - Preserve original quote characters (curly quotes “ ” ’ vs straight quotes) exactly.
2. TRAILING PUNCTUATION RULE (very important): Risk Clause headings, Dividend Policy first paragraphs, Prospectus Summary first paragraphs, and Description of Securities first paragraphs in the gold data typically do NOT include a trailing period. If the heading/paragraph ends with a period in the source, you MUST drop that final period from your extracted span. If the source ends mid-word (truncated, e.g., "combin", "operations", "stock"), keep it as-is without adding anything. When in doubt, omit the trailing period.
3. For multi-sentence spans (e.g., Risk Clauses, "(1st Para)" types), include the full block as it appears, with embedded "\n" line breaks preserved — but still strip the very last trailing period per rule 2.

Entity type guidance specific to S-1 filings:
- "Risk Clauses": Each italicized/bolded risk-factor heading sentence that introduces a risk (typically a full sentence or two stating the risk, appearing as a standalone paragraph before the explanatory text). Risk headings often appear directly above a paragraph of explanatory body text. Extract the heading paragraph(s) only, verbatim with original line breaks preserved, and STRIP the trailing period (e.g., "...national securities\nexchange" not "...national securities\nexchange."). Extract every such heading you find, even ones near the top of the passage.
- "(Header) Description of Securities", "(Header) Dividend Policy", "(Header) Prospectus Summary", "(Header) Risks To The Business": The all-caps section header text exactly as it appears (e.g., "DESCRIPTION OF SECURITIES", "DIVIDEND POLICY", "PROSPECTUS SUMMARY", "RISKS TO THE BUSINESS"). IMPORTANT: Even if the explicit all-caps header does NOT literally appear in the passage, if the passage clearly contains the first paragraph of that section (e.g., a paragraph beginning "We have not paid any cash dividends..." is the Dividend Policy section's first paragraph), you should STILL output the canonical header text (e.g., "DIVIDEND POLICY") as the header value. Use these canonical strings: "DESCRIPTION OF SECURITIES", "DIVIDEND POLICY", "PROSPECTUS SUMMARY", "RISKS TO THE BUSINESS".
- "Description of Securities (1st Para)", "Dividend Policy (1st Para)", "Prospectus Summary (1st Para)": The first paragraph of the corresponding section, verbatim with original "\n" line breaks preserved, with trailing period stripped.
  - The Dividend Policy first paragraph typically begins with "We have not paid any cash dividends on our common stock..." and continues through multiple sentences about board discretion, retention of earnings, restrictive covenants on indebtedness, and possibly a Rule 462(b) stock dividend clause maintaining sponsor ownership at 20.0%. It is one long block spanning many lines with embedded "\n" breaks. Extract the entire paragraph block as it appears.
  - These first-paragraph spans can be extracted EVEN IF the section's all-caps header is not present in the passage — recognize them by their canonical opening language and topic. The Dividend Policy paragraph may appear embedded in surrounding text without any header line preceding it.
  - When extracting these long paragraph spans, scan the entire passage for canonical opening phrases ("We have not paid any cash dividends", etc.) — do not skip them just because no header is visible.
- "Company Name", "Company Address", "Company Officer", "Company Officer Title", "Date of Prospectus", "EIN", "Amount Registered", "Max Price", "Title of Security Registered": Extract exact substring from the cover/registration info if present.
- "Agent Name", "Agent Address", "Agent Telephone": The registered agent for service of process.
- "Attorney Names", "Law Firm Name", "Law Firm Address": Counsel info.
- "Joint Book Runners": Underwriter names listed as joint book-running managers OR sole book-running manager. Look for text near phrases like "Joint Book-Running Managers", "Sole Book-Running Manager", "Sole Book -Running Manager", or underwriter names appearing standalone near the cover-page bottom (often a single word like "Cantor", "Goldman", "Morgan Stanley"). Extract the underwriter name(s) verbatim. Treat "Sole Book-Running Manager" the same as "Joint Book-Running Managers".

Procedure:
1. Output a JSON object with one key per entity type in EntityTypes, in the given order. Every key must be present; use [] when none found.
2. For each type, scan the passage and collect every matching span verbatim (preserving newlines and truncations).
3. Always check for Dividend Policy / Prospectus Summary / Description of Securities first-paragraph content even when no all-caps header is in the passage — recognize them by canonical opening sentences. If found, also emit the canonical (Header) value.
4. For Risk Clauses, identify each risk-factor heading paragraph (the standalone sentence(s) that introduce a risk, usually followed by an explanatory body paragraph). Extract each heading WITHOUT its trailing period, preserving internal "\n" line breaks.
5. For book-running managers, treat "Sole Book-Running Manager" the same as "Joint Book-Running Managers".
6. Before emitting, verify each extracted string (other than canonically inferred headers like "DIVIDEND POLICY") is a literal substring of the passage (including its embedded "\n" characters), MINUS any stripped trailing period per rule 2.
```


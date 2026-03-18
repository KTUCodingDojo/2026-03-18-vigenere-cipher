# Vigenère Cipher Kata — Problem Definition

Build a small library that can **encrypt** and **decrypt** text using a **Vigenère cipher**.
Include unit tests demonstrating each requirement with at least one passing and one failing example.

---

## Required API

- `encrypt(message, keyword)`
- `decrypt(message, keyword)`

> You may add helper functions, but keep the public API simple.

---

## Core Rules

### 1️⃣ Keyword Shift Mapping

Each letter in the keyword maps to a shift value:

- `A = 0`
- `B = 1`
- `C = 2`
- ...
- `Z = 25`

**Rule:**
Only alphabetic keyword characters are meaningful.
The implementation may assume the keyword is valid and alphabetic unless you choose to validate it explicitly.

- ✅ Pass: `B` means shift by 1
- ❌ Fail: treating `B` as shift by 2

### 2️⃣ Encryption

**Rule:**
For each letter in the message, shift it **forward** by the corresponding keyword letter’s value.
Repeat the keyword as needed.

- ✅ Pass: `encrypt("A", "B") == "B"`
- ✅ Pass: `encrypt("ABC", "B") == "BCD"`
- ❌ Fail: not repeating the key correctly

### 3️⃣ Decryption

**Rule:**
For each letter in the encrypted message, shift it **backward** by the corresponding keyword letter’s value.
Use the same keyword alignment rules as encryption.

- ✅ Pass: `decrypt("B", "B") == "A"`
- ✅ Pass: `decrypt("LXFOPVEFRNHR", "LEMON") == "ATTACKATDAWN"`
- ❌ Fail: shifting forward during decryption

### 4️⃣ Alphabet Wraparound

**Rule:**
Shifts wrap around the alphabet.

- ✅ Pass: `encrypt("Z", "B") == "A"`
- ✅ Pass: `decrypt("A", "B") == "Z"`
- ❌ Fail: shifting `Z` by 1 to a non-letter

### 5️⃣ Preserve Uppercase / Lowercase

**Rule:**
The cipher must preserve the original case of each letter.

- ✅ Pass: `encrypt("AttackAtDawn", "LEMON") == "LxfopvEfRnhr"`
- ✅ Pass: `decrypt("LxfopvEfRnhr", "LEMON") == "AttackAtDawn"`
- ❌ Fail: converting everything to uppercase or lowercase

### 6️⃣ Preserve Non-Letter Characters

**Rule:**
Spaces, digits, and punctuation remain unchanged.

- ✅ Pass: `encrypt("ATTACK AT DAWN!", "LEMON") == "LXFOPV EF RNHR!"`
- ✅ Pass: `decrypt("LXFOPV EF RNHR!", "LEMON") == "ATTACK AT DAWN!"`
- ❌ Fail: modifying spaces or punctuation

### 7️⃣ Do Not Advance Key on Non-Letters

**Rule:**
Non-letter characters do **not** consume a keyword character.
Only letters in the message advance the key position.

- ✅ Pass: `encrypt("A-A", "BC") == "B-C"`
- ✅ Pass: `decrypt("B-C", "BC") == "A-A"`
- ❌ Fail: using `B` for the first `A` and then advancing on `-`

---

## Classic Example

**Message:**

`ATTACKATDAWN`

**Key:**

`LEMON`

**Encryption:**

`LXFOPVEFRNHR`

Because:

- `A + L(11) = L`
- `T + E(4) = X`
- `T + M(12) = F`
- ...

Keyword repeated to match letters:

`LEMONLEMONLE`

---

## Acceptance Tests

```text
encrypt("A", "A") == "A"
encrypt("A", "B") == "B"
encrypt("ABC", "B") == "BCD"
encrypt("ABC", "ABC") == "ACE"

encrypt("ATTACKATDAWN", "LEMON") == "LXFOPVEFRNHR"
decrypt("LXFOPVEFRNHR", "LEMON") == "ATTACKATDAWN"

encrypt("AttackAtDawn", "LEMON") == "LxfopvEfRnhr"
decrypt("LxfopvEfRnhr", "LEMON") == "AttackAtDawn"

encrypt("ATTACK AT DAWN!", "LEMON") == "LXFOPV EF RNHR!"
decrypt("LXFOPV EF RNHR!", "LEMON") == "ATTACK AT DAWN!"

encrypt("A-A", "BC") == "B-C"
decrypt("B-C", "BC") == "A-A"
```

---

## Suggested Dojo Progression

### Round 1: Shift one uppercase character using one key letter

- `encrypt("A", "B") → "B"`
- `encrypt("Z", "B") → "A"`

### Round 2: Repeat the key for a short word

- `encrypt("ABC", "B") → "BCD"`

### Round 3: Support multi-letter keys

- `encrypt("ABC", "ABC") → "ACE"`

### Round 4: Decrypt

- `decrypt("BCD", "B") → "ABC"`

### Round 5: Preserve lowercase

- `encrypt("abc", "B") → "bcd"`

### Round 6: Preserve punctuation and spaces

- `encrypt("A A", "BC") → "B C"`

### Round 7: Do not advance key on punctuation

- `encrypt("A-A", "BC") → "B-C"`

### Round 8: Refactor to a shared transform function

At this point, `encrypt` and `decrypt` usually share most logic.

# Implementation Plan for Vigenère Cipher (By Complexity)

## Simple Implementations

### 1. Single Character Shift

**Complexity: Low**

- One character in, one character out
- Direct mapping from keyword letter to shift value
- Good starting point for the first failing tests

1. Add test for `encrypt("A", "A") == "A"`
2. Add test for `encrypt("A", "B") == "B"`
3. Add test for `encrypt("Z", "B") == "A"`
4. Implement a helper that shifts one uppercase letter forward with wraparound

### 2. Repeating a One-Letter Key

**Complexity: Low**

- Same logic reused for each character
- No key indexing complexity yet

1. Add test for `encrypt("ABC", "B") == "BCD"`
2. Implement looping through the message
3. Reuse the same shift for every letter when the key has length 1

### 3. Multi-Letter Key

**Complexity: Low-Medium**

- Introduces key indexing
- Requires repeating the key when the message is longer

1. Add test for `encrypt("ABC", "ABC") == "ACE"`
2. Add test for classic key repetition behavior
3. Implement key position tracking for letter characters

## Moderate Implementations

### 4. Decryption

**Complexity: Medium**

- Same structure as encryption
- Main difference is reversing the shift direction

1. Add test for `decrypt("BCD", "B") == "ABC"`
2. Add test for `decrypt("LXFOPVEFRNHR", "LEMON") == "ATTACKATDAWN"`
3. Refactor shared logic so encrypt/decrypt differ only by direction

### 5. Preserve Letter Case

**Complexity: Medium**

- Requires case-aware transformation
- Logic must preserve original letter shape while still shifting correctly

1. Add test for `encrypt("abc", "B") == "bcd"`
2. Add test for `encrypt("AttackAtDawn", "LEMON") == "LxfopvEfRnhr"`
3. Add test for matching decryption with mixed case
4. Implement separate handling for uppercase and lowercase letters

### 6. Preserve Spaces and Punctuation

**Complexity: Medium**

- Non-letter characters must pass through unchanged
- Still simple as long as key advancement is not yet coupled to them

1. Add test for `encrypt("ATTACK AT DAWN!", "LEMON") == "LXFOPV EF RNHR!"`
2. Add test for matching decryption with spaces and punctuation
3. Return non-letter characters unchanged

## Complex Implementations

### 7. Do Not Advance Key on Non-Letters

**Complexity: High**

- This is the first place where message position and key position differ
- Easy place for off-by-one mistakes

1. Add test for `encrypt("A-A", "BC") == "B-C"`
2. Add test for `decrypt("B-C", "BC") == "A-A"`
3. Ensure only alphabetic message characters advance the key index
4. Verify spaces, punctuation, and digits do not consume key characters

### 8. Shared Transform Function

**Complexity: High**

- Improves design rather than adding new behavior
- Helps keep encryption and decryption consistent

1. Extract a shared transform function with a direction parameter
2. Keep public API as `encrypt(message, keyword)` and `decrypt(message, keyword)`
3. Remove duplication between both methods
4. Keep tests unchanged during refactoring

### 9. Validation and Edge Cases (Optional)

**Complexity: High**

- Useful if the dojo wants to discuss input assumptions
- Can be skipped if the kata assumes valid input

1. Decide what to do with empty message
2. Decide what to do with empty keyword
3. Decide whether to reject non-alphabetic keyword characters
4. Add tests only if validation is part of the chosen scope

## Implementation Priority Order (By Complexity)

1. Single Character Shift
2. Repeating a One-Letter Key
3. Multi-Letter Key
4. Decryption
5. Preserve Letter Case
6. Preserve Spaces and Punctuation
7. Do Not Advance Key on Non-Letters
8. Shared Transform Function
9. Validation and Edge Cases (optional)

## Success Criteria

- All acceptance tests pass
- Encryption and decryption both work with the same keyword rules
- Case is preserved correctly
- Non-letter characters stay unchanged
- Non-letter characters do not advance the key
- Code remains easy to refactor and explain during a dojo

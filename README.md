# SCANF

**SCANF** (_**S**tatic **C**ode **A**nalysis a**N**d **F**ixes_) is a code analysis package build using .Net Compiler SDK. The development is still in early stages and this document would be updated soon.

## What is a Static Code analyzer

Static Code Analysis allows you to inspect and examine code before execution (during design time) for possible violation of quality, naming convention/style and other issues.

If a violation is found by the analyzer, it would be reported in Error List Window as well as marked with a squiggle in the Editor. The Code analyzer could include one or more suggestive code fixes for the issue reported, which the developer could apply to correct the violation.

## Rules Support

Current list of rules implemented are listed at https://anuviswan.github.io/scanf/ . Summary of the same is given below.

| Code    | Description                                                        |            CodeFix | Analyzer Category |
| ------- | ------------------------------------------------------------------ | -----------------: | ----------------- |
| SF 1002 | Avoid Empty Methods                                                | :heavy_check_mark: | Code Smell        |
| SF 1003 | Pending TODO Item                                                  |                :x: | Code Smell        |
| SF 1004 | Pure methods should return value                                   |                :x: | Code Smell        |
| SF 1005 | Async Methods should not return void                               | :heavy_check_mark: | Code Smell        |
| SF 1008 | Rename method with Async Suffix                                    | :heavy_check_mark: | Naming Convention |
| SF 1009 | Constructor should not invoke virtual method                       |                :x: | Bug               |
| SF 1010 | Custom Exceptions should be always public                          | :heavy_check_mark: | Code Smell        |
| SF 1011 | Enums with Flag Attribute should have its values as the power of 2 |                :x: | Bug               |

# SCANF

**SCANF** (_**S**tatic **C**ode **A**nalysis a**N**d **F**ixes_) is a code analysis package for Rosylyn. The development is still in early stages and this document would be updated soon.

Current list of rules implemented are listed at https://anuviswan.github.io/scanf/ . Summary of the same is given below.

| Code    | Description                                  |  CodeFix Available | Analyzer Category |
| ------- | -------------------------------------------- | -----------------: | ----------------- |
| SF 1002 | Avoid Empty Methods                          | :heavy_check_mark: | Code Smell        |
| SF 1003 | Pending TODO Item                            |                :x: | Code Smell        |
| SF 1004 | Pure methods should return value             |                :x: | Code Smell        |
| SF 1005 | Async Methods should not return void         |                :x: | Code Smell        |
| SF 1008 | Rename method with Async Suffix              |                :x: | Naming Convention |
| SF 1009 | Constructor should not invoke virtual method |                :x: | Code Smell        |

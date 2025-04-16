```mermaid
classDiagram
class LevelManipulator  {
<<class>>
+Disassemble(Level level)
+Filter[,] AsGameMatrix(Level level)
}
class LevelChecker {
<<class>>
+SimulateAsMatrix(Level level)
+Result CalculateResult(Level level)
}

class Level {
<<class>>
+int Width
+int Height
+Block[] Blocks
}

class Block{
<<class>>
+int Offset
+BlockType Type
+bool IsStatic
+Filter[] Filters
+bool IsInStorage()
+Move(int offset)
}

class Filter{
<<class>>
+byte ColorId
+byte ShapeId
+bool IsBroken
+Break()
+Fix()
}

class BlockType{
<<enumeration>>
Producer
Converter
Receiver
}

class Result{
<<class>>
+Filter?[,] Matrix
+float Score
}

    Level o-- Block
    Block *-- BlockType
    Block o-- Filter
    Result o-- Filter

    LevelManipulator ..> Level

    LevelChecker ..> Level
    LevelChecker ..> Filter
    LevelChecker ..> Result

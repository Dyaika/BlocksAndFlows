```mermaid
classDiagram
class LevelManipulator  {
<<class>>
+Disassemble(Level level)
+Filter[,] AsGameMatrix(Level level)
+MoveBlock(Level level, Block block)
+Hint(Level current, Level solution)
}
class LevelChecker {
<<class>>
+Filter[,] SimulateAsMatrix(Level level)
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

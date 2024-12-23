namespace citrus.Parsing.AST;

public class CaseNode : ASTNode
{
    public CaseNode() : base(ASTNodeType.Case) { }
    public ASTNode? TestValue { get; set; }
    public List<ASTNode?> ElseBody { get; set; } = [];
    public List<ASTNode?> WhenNodes { get; set; } = [];

    public override void Print(int depth)
    {
        ASTTracer.PrintDepth(depth);
        PrintASTNodeType();

        if (TestValue != null)
        {
            ASTTracer.PrintDepth(1 + depth);
            Console.WriteLine("Test:");
            TestValue.Print(2 + depth);
        }

        if (WhenNodes.Count > 0)
        {
            foreach (var when in WhenNodes)
            {
                when?.Print(1 + depth);
            }
        }

        if (ElseBody.Count > 0)
        {
            ASTTracer.PrintDepth(1 + depth);
            Console.WriteLine("Case else:");
            foreach (var stmt in ElseBody)
            {
                stmt?.Print(2 + depth);
            }
        }
    }

    public override ASTNode Clone()
    {
        List<ASTNode?> clonedElseBody = [];
        foreach (var stmt in ElseBody)
        {
            clonedElseBody.Add(stmt?.Clone());
        }

        List<ASTNode?> clonedWhenNodes = [];
        foreach (var when in WhenNodes)
        {
            clonedWhenNodes.Add(when?.Clone());
        }

        return new CaseNode
        {
            TestValue = TestValue?.Clone(),
            ElseBody = clonedElseBody,
            WhenNodes = clonedWhenNodes
        };
    }
}
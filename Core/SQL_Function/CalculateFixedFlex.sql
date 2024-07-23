CREATE OR ALTER FUNCTION CalculateFixedFlexFromExpenseAndType
(
    @expenseId INT,
	@expenseType INT
)
RETURNS TABLE
AS
RETURN
(
    WITH TotalAmount AS
    (
        SELECT amount, personID 
        FROM Expense 
        WHERE Id = @expenseId
    ),
	PeopleCount AS
    (
        SELECT COUNT(DISTINCT PersonId) AS TotalNumOfPeople 
        FROM PersonExpense 
        WHERE ExpenseID = @expenseId
    )
    SELECT pe.PersonId AS PersonPayId, 
           pp.name AS PersonPayName, 
           ROUND(e.amount / pc.TotalNumOfPeople, 2) AS Amount,
           e.personID AS PersonReceiveId,
           pr.name AS PersonReceiveName
    FROM PersonExpense pe
    JOIN Person pp ON pe.PersonId = pp.ID
    CROSS JOIN TotalAmount e
    CROSS JOIN PeopleCount pc
    JOIN Person pr ON e.personID = pr.ID
    WHERE pe.ExpenseID = @expenseId 
      AND pe.PersonId <> e.personID
);
GO

-- Example call
SELECT * FROM CalculateFixedFlex(8, 1);
SELECT * FROM CalculateFixedFlex(5, 0);
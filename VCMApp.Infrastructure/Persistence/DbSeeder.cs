using Microsoft.EntityFrameworkCore;

namespace VCMApp.Infrastructure.Persistence
{
    public static class DbSeeder
    {
        public static void Seed(VCMDbContext context)
        {
            context.Database.Migrate();

            if (!context.Categories.Any())
            {
                context.Database.ExecuteSqlRaw(@"
                    SET IDENTITY_INSERT [dbo].[Categories] ON 

                    INSERT [dbo].[Categories] ([Id], [Name], [CreatedBy]) VALUES (1, N'IT', N'ADMIN')
                    INSERT [dbo].[Categories] ([Id], [Name], [CreatedBy]) VALUES (2, N'Marketing', N'ADMIN')
                    INSERT [dbo].[Categories] ([Id], [Name], [CreatedBy]) VALUES (3, N'Sales', N'ADMIN')
                    SET IDENTITY_INSERT [dbo].[Categories] OFF
                ");
            }

            if (!context.ExamQuestions.Any())
            {
                context.Database.ExecuteSqlRaw(@"
                SET IDENTITY_INSERT [dbo].[ExamQuestions] ON 

                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (1, 1, N'What is polymorphism in OOP?', 1, N'ADMIN')
                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (2, 1, N'Explain the concept of inheritance.', 1, N'ADMIN')
                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (3, 2, N'What is a marketing funnel?', 1, N'ADMIN')
                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (4, 2, N'Describe the role of HR in employee retention.', 1, N'ADMIN')
                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (5, 1, N'What does HTML stand for?', 1, N'ADMIN')
                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (6, 1, N'Which of the following is a valid variable name in Python?', 1, N'ADMIN')
                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (7, 1, N'What is the purpose of a loop in programming?', 1, N'ADMIN')
                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (8, 1, N'Which of the following is not a programming language?', 1, N'ADMIN')
                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (9, 1, N'What does the “if” statement do in programming?', 1, N'ADMIN')
                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (10, 1, N'Which data structure uses LIFO (Last In, First Out)?', 1, N'ADMIN')
                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (11, 1, N'What is the output of print(2 + 3 * 4) in Python?', 1, N'ADMIN')
                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (13, 1, N'What is the extension for a Python file?', 1, N'ADMIN')
                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (14, 1, N'Which of the following is a backend programming language?', 1, N'ADMIN')
                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (15, 1, N'In object-oriented programming, what is a class?', 1, N'ADMIN')
                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (16, 1, N'Which operator is used for comparison in most programming languages?', 1, N'ADMIN')
                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (17, 1, N'What does SQL stand for?', 1, N'ADMIN')
                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (18, 1, N'Which of these is a valid loop in JavaScript?', 1, N'ADMIN')
                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (19, 1, N'What is the purpose of a function in programming?', 1, N'ADMIN')
                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (20, 1, N'Which of these is not a valid data type in JavaScript?', 1, N'ADMIN')
                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (21, 1, N'What is the result of 5 % 2 in most programming languages?', 1, N'ADMIN')
                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (22, 1, N'What does CSS stand for?', 1, N'ADMIN')
                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (23, 1, N'What is the purpose of an IDE?', 1, N'ADMIN')
                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (24, 1, N'Which of the following is a version control system?', 1, N'ADMIN')

                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (25, 2, N'What is the primary purpose of a SWOT analysis in marketing?', 1, N'ADMIN')
                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (26, 2, N'Which of the following is an example of demographic segmentation?', 1, N'ADMIN')
                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (27, 2, N'What does PPC stand for in digital marketing?', 1, N'ADMIN')
                INSERT [dbo].[ExamQuestions] ([Id], [CategoryId], [QuestionContent], [QuestionPoint], [CreatedBy]) VALUES (28, 2, N'Which of the following is NOT a component of the marketing mix (4Ps)?', 1, N'ADMIN')

                SET IDENTITY_INSERT [dbo].[ExamQuestions] OFF
                ");
            }

            if (!context.Vacancies.Any())
            {
                context.Database.ExecuteSqlRaw(@"
                SET IDENTITY_INSERT [dbo].[Vacancies] ON 

                INSERT [dbo].[Vacancies] ([Id], [CategoryId], [Name], [Title], [Content], [StartDate], [EndDate], [IsActive], [CreatedBy]) VALUES (1, 1, N'Junior Software Developer', N'Exciting Junior Developer Position', N'Join our team as a junior developer...', CAST(N'2025-01-24T18:35:07.9200000' AS DateTime2), CAST(N'2025-02-24T18:35:07.9200000' AS DateTime2), 1, N'ADMIN')
                INSERT [dbo].[Vacancies] ([Id], [CategoryId], [Name], [Title], [Content], [StartDate], [EndDate], [IsActive], [CreatedBy]) VALUES (2, 2, N'Marketing Specialist', N'Dynamic Marketing Role', N'Looking for a creative marketing specialist...', CAST(N'2025-01-20T18:35:07.9200000' AS DateTime2), CAST(N'2025-02-24T18:35:07.9200000' AS DateTime2), 1, N'ADMIN')
                SET IDENTITY_INSERT [dbo].[Vacancies] OFF
                ");
            }

            if (!context.ExamQuestionOptions.Any())
            {
                context.Database.ExecuteSqlRaw(@"
                SET IDENTITY_INSERT [dbo].[ExamQuestionOptions] ON 

                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (1, 1, N'The ability to take many forms.', 1, 1, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (2, 1, N'A type of data structure.', 2, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (3, 1, N'2 A type of data structure.', 3, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (4, 2, N'Reusing properties from another class.', 1, 1, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (5, 2, N'A way to store data.', 2, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (6, 2, N'A process for guiding customers.', 3, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (7, 5, N'HyperText Markup Language', 1, 1, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (8, 5, N'HighText Machine Language', 1, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (9, 5, N'HyperTool Multi Language', 1, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (10, 6, N'2variable', 1, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (11, 6, N'variable_2', 1, 1, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (12, 6, N'variable-2', 1, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (13, 7, N'To execute a block of code repeatedly', 1, 1, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (14, 7, N'To store multiple values', 2, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (15, 7, N'To define a function', 3, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (16, 8, N'Java', 1, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (17, 8, N'HTML', 2, 1, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (18, 8, N'Python', 3, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (19, 9, N'Declares a new variable', 1, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (20, 9, N'Executes a block of code if a condition is true', 2, 1, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (21, 9, N'Ends a loop', 3, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (22, 10, N'Queue', 1, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (23, 10, N'Stack', 2, 1, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (24, 10, N'Array', 3, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (25, 11, N'20', 1, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (26, 11, N'14', 2, 1, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (27, 11, N'24', 3, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (28, 13, N'.py', 1, 1, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (29, 13, N'.java', 2, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (30, 13, N'.html', 3, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (31, 14, N'CSS', 1, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (32, 14, N'JavaScript', 2, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (33, 14, N'PHP', 3, 1, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (34, 15, N'A function inside a program', 1, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (35, 15, N'A blueprint for creating objects', 2, 1, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (36, 15, N'A variable that stores data', 3, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (37, 16, N'=', 1, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (38, 16, N'==', 2, 1, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (39, 16, N'===', 3, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (40, 17, N'Structured Query Language', 1, 1, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (41, 17, N'Simple Query Language', 2, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (42, 17, N'Sequential Query Language', 3, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (43, 18, N'for', 1, 1, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (44, 18, N'repeat', 2, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (45, 18, N'do-repeat', 3, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (46, 19, N'To store data', 1, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (47, 19, N'To execute a block of code when called', 2, 1, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (48, 19, N'To define a variable', 3, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (49, 20, N'Number', 1, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (50, 20, N'Boolean', 2, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (51, 20, N'Float', 3, 1, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (52, 21, N'2', 1, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (53, 21, N'1', 2, 1, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (54, 21, N'0', 3, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (55, 22, N'Cascading Style Sheets', 1, 1, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (56, 22, N'Creative Style Scripts', 2, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (57, 22, N'Computer Styling System', 3, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (58, 23, N'To provide tools for writing, testing, and debugging code', 1, 1, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (59, 23, N'To store data in a database', 2, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (60, 23, N'To design graphics for a program', 3, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (61, 24, N'Git', 1, 1, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (62, 24, N'Node.js', 2, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (63, 24, N'MySQL', 3, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (64, 3, N'1', 1, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (65, 3, N'2', 2, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (66, 3, N'3', 3, 1, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (67, 4, N'1', 1, 1, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (68, 4, N'2', 2, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (69, 4, N'3', 3, 0, N'ADMIN')

                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (70, 25, N'To identify the target audience', 1, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (71, 25, N'To evaluate the strengths, weaknesses, opportunities, and threats of a business', 2, 1, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (72, 25, N'To determine pricing strategies', 3, 0, N'ADMIN')               
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (73, 26, N'Targeting customers based on their hobbies', 1, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (74, 26, N'Segmenting the market by income level', 2, 1, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (75, 26, N'Grouping customers by their location', 3, 0, N'ADMIN')     
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (76, 27, N'Pay Per Campaign', 1, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (77, 27, N'Product Promotion Cost', 2, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (78, 27, N'Pay Per Click', 3, 1, N'ADMIN')                 
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (79, 28, N'Product', 1, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (80, 28, N'Promotion', 2, 0, N'ADMIN')
                INSERT [dbo].[ExamQuestionOptions] ([Id], [ExamQuestionId], [Content], [PriorityNumber], [IsCorrect], [CreatedBy]) VALUES (81, 28, N'Process', 3, 1, N'ADMIN')    

                SET IDENTITY_INSERT [dbo].[ExamQuestionOptions] OFF
                ");
            }

            if (!context.VacancyExamQuestions.Any())
            {
                context.Database.ExecuteSqlRaw(@"
                SET IDENTITY_INSERT [dbo].[VacancyExamQuestions] ON 

                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (1, 1, 1, N'ADMIN')
                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (2, 1, 2, N'ADMIN')
                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (3, 2, 3, N'ADMIN')
                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (4, 2, 4, N'ADMIN')
                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (6, 1, 5, N'ADMIN')
                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (7, 1, 6, N'ADMIN')
                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (8, 1, 7, N'ADMIN')
                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (9, 1, 8, N'ADMIN')
                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (10, 1, 9, N'ADMIN')
                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (11, 1, 10, N'ADMIN')
                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (12, 1, 11, N'ADMIN')
                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (13, 1, 13, N'ADMIN')
                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (14, 1, 14, N'ADMIN')
                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (15, 1, 15, N'ADMIN')
                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (16, 1, 16, N'ADMIN')
                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (17, 1, 17, N'ADMIN')
                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (18, 1, 18, N'ADMIN')
                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (19, 1, 19, N'ADMIN')
                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (20, 1, 20, N'ADMIN')
                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (21, 1, 21, N'ADMIN')
                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (22, 1, 22, N'ADMIN')
                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (23, 1, 23, N'ADMIN')
                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (24, 1, 24, N'ADMIN')

                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (25, 2, 25, N'ADMIN')
                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (26, 2, 26, N'ADMIN')
                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (27, 2, 27, N'ADMIN')
                INSERT [dbo].[VacancyExamQuestions] ([Id], [VacancyId], [ExamQuestionId], [CreatedBy]) VALUES (28, 2, 28, N'ADMIN')
                SET IDENTITY_INSERT [dbo].[VacancyExamQuestions] OFF
                ");
            }

            if (!context.VacancyExamDetails.Any())
            {
                context.Database.ExecuteSqlRaw(@"
                SET IDENTITY_INSERT [dbo].[VacancyExamDetails] ON 

                INSERT [dbo].[VacancyExamDetails] ([Id], [VacancyId], [ExamQuestionCount], [ExamQuestionTimeInMinute], [TotalTimeOfTestInMinute]) VALUES (2, 1, 10, 1, 10)
                INSERT [dbo].[VacancyExamDetails] ([Id], [VacancyId], [ExamQuestionCount], [ExamQuestionTimeInMinute], [TotalTimeOfTestInMinute]) VALUES (3, 2, 5, 1, 5)
                SET IDENTITY_INSERT [dbo].[VacancyExamDetails] OFF
                ");
            }
        }
    }
}

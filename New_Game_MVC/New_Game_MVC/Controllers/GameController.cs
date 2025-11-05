using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using New_Game_MVC.Models;
using System;

namespace New_Game_MVC.Controllers
{
        public class GameController : Controller
        {
            private readonly AppDbContext _context;

            public GameController(AppDbContext context)
            {
                _context = context;
            }

            public async Task<IActionResult> Index()
            {
                var model = new GameViewModel
                {
                    GameHistory = await _context.GameResults
                        .OrderByDescending(g => g.GameDate)
                        .Take(10)
                        .ToListAsync()
                };
                CalculateStats(model);
                return View(model);
            }

            [HttpPost]
            public async Task<IActionResult> Play(Choice playerChoice)
            {
                // اختيار الكمبيوتر العشوائي
                var computerChoice = (Choice)new Random().Next(0, 3);

                // تحديد النتيجة
                var result = DetermineWinner(playerChoice, computerChoice);
                var resultText = GetResultText(result);

                // حفظ النتيجة في قاعدة البيانات
                var gameResult = new GameResult
                {
                    PlayerChoice = playerChoice,
                    ComputerChoice = computerChoice,
                    Result = result.ToString()
                };

                _context.GameResults.Add(gameResult);
                await _context.SaveChangesAsync();

                // إعداد النموذج للعرض
                var model = new GameViewModel
                {
                    PlayerChoice = playerChoice,
                    GameResult = $"أنت اخترت: {playerChoice} | الكمبيوتر اختار: {computerChoice} | النتيجة: {resultText}",
                    GameHistory = await _context.GameResults
                        .OrderByDescending(g => g.GameDate)
                        .Take(10)
                        .ToListAsync()
                };
                CalculateStats(model);

                return View("Index", model);
            }

            private string DetermineWinner(Choice player, Choice computer)
            {
                if (player == computer) return "Draw";

                if ((player == Choice.Rock && computer == Choice.Scissors) ||
                    (player == Choice.Paper && computer == Choice.Rock) ||
                    (player == Choice.Scissors && computer == Choice.Paper))
                {
                    return "Win";
                }

                return "Lose";
            }
        private string GetResultText(string result)
        {
            return result switch
            {
                "Win" => "فوز 🎉",
                "Lose" => "خسارة 😞",
                "Draw" => "تعادل 🤝",
                _ => "غير معروف"
            };
        }

        private void CalculateStats(GameViewModel model)
        {
            model.Wins = _context.GameResults.Count(g => g.Result == "Win");
            model.Losses = _context.GameResults.Count(g => g.Result == "Lose");
            model.Draws = _context.GameResults.Count(g => g.Result == "Draw");
        }
    }
}
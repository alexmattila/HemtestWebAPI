using Microsoft.AspNetCore.Mvc;

namespace HemtestWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FrequentWordCountController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] string text)
        {
            var Words = text.Split(' ');
            Dictionary<string, int> FrequentWordCount = new(StringComparer.CurrentCultureIgnoreCase);

            for (int i = 0; i < Words.Length; i++)
            {
                if (FrequentWordCount.ContainsKey(Words[i]))
                {
                    int value = FrequentWordCount[Words[i]];
                    FrequentWordCount[Words[i]] = value + 1;
                }
                else
                {
                    FrequentWordCount.Add(Words[i], 1);
                }
            }

            var SortedWords = new Dictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);

            foreach (var Word in FrequentWordCount.OrderByDescending(x => x.Value).Take(10))
            {
                SortedWords.Add(Word.Key, Word.Value);
            }

            return Ok(SortedWords);
        }
    }
}
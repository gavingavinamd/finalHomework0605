using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MapleStoryFinal.Models;
using MapleStoryFinal.Models.ViewModel;

namespace MapleStoryFinal.Controllers
{
    public class TblMonstersController : Controller
    {
        private readonly MapleStoryDBContext _context;

        public TblMonstersController(MapleStoryDBContext context)
        {
            _context = context;
        }

        // GET: TblMonsters
        public async Task<IActionResult> Index()
        {
              return _context.TblMonsters != null ? 
                          View(await _context.TblMonsters.ToListAsync()) :
                          Problem("Entity set 'MapleStoryDBContext.TblMonsters'  is null.");
        }

        // GET: TblMonsters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblMonsters == null)
            {
                return NotFound();
            }

            var tblMonster = await _context.TblMonsters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblMonster == null)
            {
                return NotFound();
            }

            return View(tblMonster);
        }

        // GET: TblMonsters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblMonsters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Mlevel,Atk,Hp,Area")] TblMonster tblMonster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblMonster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblMonster);
        }

        // GET: TblMonsters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblMonsters == null)
            {
                return NotFound();
            }

            var tblMonster = await _context.TblMonsters.FindAsync(id);
            if (tblMonster == null)
            {
                return NotFound();
            }
            return View(tblMonster);
        }

        // POST: TblMonsters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Mlevel,Atk,Hp,Area")] TblMonster tblMonster)
        {
            if (id != tblMonster.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblMonster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblMonsterExists(tblMonster.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tblMonster);
        }

        // GET: TblMonsters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblMonsters == null)
            {
                return NotFound();
            }

            var tblMonster = await _context.TblMonsters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblMonster == null)
            {
                return NotFound();
            }

            return View(tblMonster);
        }

        // POST: TblMonsters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblMonsters == null)
            {
                return Problem("Entity set 'MapleStoryDBContext.TblMonsters'  is null.");
            }
            var tblMonster = await _context.TblMonsters.FindAsync(id);
            if (tblMonster != null)
            {
                _context.TblMonsters.Remove(tblMonster);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblMonsterExists(int id)
        {
          return (_context.TblMonsters?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet]
        public IActionResult Search()
        {
            ViewData["Message"] = "搜尋怪物，空頁面";
            return View(new MonsterSearchViewModel());
        }
        
        [HttpPost]
        public IActionResult Search(MonsterSearchParms monsterSearchParms)
        {
            var viewModel = new MonsterSearchViewModel();
            var searchResult = _context.TblMonsters.ToList();

            searchResult = _context.TblMonsters
                .Where(x => x.Mlevel >= monsterSearchParms.minlevel && x.Mlevel <= monsterSearchParms.maxlevel).ToList();
            
            viewModel.SearchParms = monsterSearchParms; 
            viewModel.Monsters = searchResult.ToList();


            return View(viewModel);
        }
    }
}

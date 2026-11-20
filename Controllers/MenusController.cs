
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurante.Models;
using Restaurante.Data;

public class MenusController : Controller
{
    private readonly RestauranteDbContext _context;

    public MenusController(RestauranteDbContext context)
    {
        _context = context;
    }

    // GET: MENUS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Menus.ToListAsync());
    }

    // GET: MENUS/Details/5
    public async Task<IActionResult> Details(int? menuid)
    {
        if (menuid == null)
        {
            return NotFound();
        }

        var menu = await _context.Menus
            .FirstOrDefaultAsync(m => m.MenuId == menuid);
        if (menu == null)
        {
            return NotFound();
        }

        return View(menu);
    }

    // GET: MENUS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: MENUS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MenuId,Nombre,Descripcion,Precio,Disponible,Detalleordenes")] Menu menu)
    {
        if (ModelState.IsValid)
        {
            _context.Add(menu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(menu);
    }

    // GET: MENUS/Edit/5
    public async Task<IActionResult> Edit(int? menuid)
    {
        if (menuid == null)
        {
            return NotFound();
        }

        var menu = await _context.Menus.FindAsync(menuid);
        if (menu == null)
        {
            return NotFound();
        }
        return View(menu);
    }

    // POST: MENUS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? menuid, [Bind("MenuId,Nombre,Descripcion,Precio,Disponible,Detalleordenes")] Menu menu)
    {
        if (menuid != menu.MenuId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(menu);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(menu.MenuId))
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
        return View(menu);
    }

    // GET: MENUS/Delete/5
    public async Task<IActionResult> Delete(int? menuid)
    {
        if (menuid == null)
        {
            return NotFound();
        }

        var menu = await _context.Menus
            .FirstOrDefaultAsync(m => m.MenuId == menuid);
        if (menu == null)
        {
            return NotFound();
        }

        return View(menu);
    }

    // POST: MENUS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? menuid)
    {
        var menu = await _context.Menus.FindAsync(menuid);
        if (menu != null)
        {
            _context.Menus.Remove(menu);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MenuExists(int? menuid)
    {
        return _context.Menus.Any(e => e.MenuId == menuid);
    }
}

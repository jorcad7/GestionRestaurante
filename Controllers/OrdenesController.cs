
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurante.Models;
using Restaurante.Data;

public class OrdenesController : Controller
{
    private readonly RestauranteDbContext _context;

    public OrdenesController(RestauranteDbContext context)
    {
        _context = context;
    }

    // GET: ORDENES
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Ordenes.ToListAsync());
    }

    // GET: ORDENES/Details/5
    public async Task<IActionResult> Details(int? ordenid)
    {
        if (ordenid == null)
        {
            return NotFound();
        }

        var ordene = await _context.Ordenes
            .FirstOrDefaultAsync(m => m.OrdenId == ordenid);
        if (ordene == null)
        {
            return NotFound();
        }

        return View(ordene);
    }

    // GET: ORDENES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ORDENES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("OrdenId,ClienteId,FechaOrden,Estado,Total,Cliente,Detalleordenes")] Ordene ordene)
    {
        if (ModelState.IsValid)
        {
            _context.Add(ordene);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(ordene);
    }

    // GET: ORDENES/Edit/5
    public async Task<IActionResult> Edit(int? ordenid)
    {
        if (ordenid == null)
        {
            return NotFound();
        }

        var ordene = await _context.Ordenes.FindAsync(ordenid);
        if (ordene == null)
        {
            return NotFound();
        }
        return View(ordene);
    }

    // POST: ORDENES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? ordenid, [Bind("OrdenId,ClienteId,FechaOrden,Estado,Total,Cliente,Detalleordenes")] Ordene ordene)
    {
        if (ordenid != ordene.OrdenId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(ordene);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdeneExists(ordene.OrdenId))
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
        return View(ordene);
    }

    // GET: ORDENES/Delete/5
    public async Task<IActionResult> Delete(int? ordenid)
    {
        if (ordenid == null)
        {
            return NotFound();
        }

        var ordene = await _context.Ordenes
            .FirstOrDefaultAsync(m => m.OrdenId == ordenid);
        if (ordene == null)
        {
            return NotFound();
        }

        return View(ordene);
    }

    // POST: ORDENES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? ordenid)
    {
        var ordene = await _context.Ordenes.FindAsync(ordenid);
        if (ordene != null)
        {
            _context.Ordenes.Remove(ordene);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool OrdeneExists(int? ordenid)
    {
        return _context.Ordenes.Any(e => e.OrdenId == ordenid);
    }
}

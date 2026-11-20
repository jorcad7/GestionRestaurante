
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurante.Models;
using Restaurante.Data;

public class DetalleordenesController : Controller
{
    private readonly RestauranteDbContext _context;

    public DetalleordenesController(RestauranteDbContext context)
    {
        _context = context;
    }

    // GET: DETALLEORDENES
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Detalleordenes.ToListAsync());
    }

    // GET: DETALLEORDENES/Details/5
    public async Task<IActionResult> Details(int? detalleid)
    {
        if (detalleid == null)
        {
            return NotFound();
        }

        var detalleordene = await _context.Detalleordenes
            .FirstOrDefaultAsync(m => m.DetalleId == detalleid);
        if (detalleordene == null)
        {
            return NotFound();
        }

        return View(detalleordene);
    }

    // GET: DETALLEORDENES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: DETALLEORDENES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("DetalleId,OrdenId,MenuId,Cantidad,PrecioUnitario,Menu,Orden")] Detalleordene detalleordene)
    {
        if (ModelState.IsValid)
        {
            _context.Add(detalleordene);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(detalleordene);
    }

    // GET: DETALLEORDENES/Edit/5
    public async Task<IActionResult> Edit(int? detalleid)
    {
        if (detalleid == null)
        {
            return NotFound();
        }

        var detalleordene = await _context.Detalleordenes.FindAsync(detalleid);
        if (detalleordene == null)
        {
            return NotFound();
        }
        return View(detalleordene);
    }

    // POST: DETALLEORDENES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? detalleid, [Bind("DetalleId,OrdenId,MenuId,Cantidad,PrecioUnitario,Menu,Orden")] Detalleordene detalleordene)
    {
        if (detalleid != detalleordene.DetalleId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(detalleordene);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleordeneExists(detalleordene.DetalleId))
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
        return View(detalleordene);
    }

    // GET: DETALLEORDENES/Delete/5
    public async Task<IActionResult> Delete(int? detalleid)
    {
        if (detalleid == null)
        {
            return NotFound();
        }

        var detalleordene = await _context.Detalleordenes
            .FirstOrDefaultAsync(m => m.DetalleId == detalleid);
        if (detalleordene == null)
        {
            return NotFound();
        }

        return View(detalleordene);
    }

    // POST: DETALLEORDENES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? detalleid)
    {
        var detalleordene = await _context.Detalleordenes.FindAsync(detalleid);
        if (detalleordene != null)
        {
            _context.Detalleordenes.Remove(detalleordene);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool DetalleordeneExists(int? detalleid)
    {
        return _context.Detalleordenes.Any(e => e.DetalleId == detalleid);
    }
}

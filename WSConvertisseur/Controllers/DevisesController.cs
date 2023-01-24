using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using WSConvertisseur.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WSConvertisseur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevisesController : ControllerBase
    {

        public List<Devise> devises;
        /// <summary>
        /// Constructeur vide du controller DevisesController.
        /// </summary>
        public DevisesController()
        {
            devises = new() {new(1, "Dollar", 1.08), new(2, "Franc Suisse", 1.07), new(3, "Yen", 120) };
        }
        // GET: api/<DevisesController>
        /// <summary>
        /// Renvoie toutes les devises.
        /// </summary>
        /// <returns>List<Devise></returns>
        [HttpGet]
        public IEnumerable<Devise> GetAll()
        {
            return devises;
        }
        /// <summary>
        /// Renvoie la devise correspndant à l'id passé en paramètre.
        /// </summary>
        /// <param name="id">Identifiant de la devise qui sera retournée.</param>
        /// <returns>Devise</returns>
        /// <response code="404">Retourne Not found si l'id passé ne correspond à aucune devise.</response>
        // GET api/<DevisesController>/5
        [HttpGet("{id}", Name = "GetDevise")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Devise> GetById(int id)
        {
            Devise? devise = devises.FirstOrDefault(x => x.Id == id);
            return devise == null ? NotFound():devise;
        }
        /// <summary>
        /// Ajoute à la liste la devise passée dans le body
        /// </summary>
        /// <param name="devise">devise sous forme de json à passer dans le body</param>
        /// <returns>retourne la devise crée.</returns>
        /// <response code="400">Retoune bad request si le format de la devise n'est pas bon.</response>
        // POST api/<DevisesController>
        [HttpPost]
        public ActionResult<Devise> Post([FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            devises.Add(devise);
            return CreatedAtRoute("GetDevise", new { id = devise.Id }, devise);
        }
        /// <summary>
        /// Modifie la devise avec l'id en paramètre pour la devise passée en json dans le body.
        /// </summary>
        /// <param name="id">id de la devise que vous voulez modifier.</param>
        /// <param name="devise">nouvelle devise passée en json dans le body.</param>
        /// <returns>rien</returns>
        /// <response code="400">retourne une erreur bad request si la devise en paramètre n'est pas bien formattée ou que la devise en paramètre est différent de l'id passé.</response>
        /// <response code="404">retroune not found si l'id ne correspond à aucune devise dans la liste.</response>
        // PUT api/<DevisesController>/5
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Devise devise)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != devise.Id)
            {
                return BadRequest();
            }
            int index = devises.FindIndex((d) => d.Id == id);
            if (index < 0)
            {
                return NotFound();
            }
            devises[index] = devise;
            return NoContent();

        }
        /// <summary>
        /// supprime la devise correspondant à l'id en parametre.
        /// </summary>
        /// <param name="id">id de la devise que vous voulez supprimer.</param>
        /// <response code="404">retourne not found si l'id en paramètre ne correspond à aucune devise.</response>
        /// <returns>retourne la devise correspondant à l'id en paramètre.</returns>
        // DELETE api/<DevisesController>/5

        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        public ActionResult<Devise> Delete(int id)
        {
            Devise devise = devises.FirstOrDefault(x => x.Id == id);
            if (devise == null)
                return NotFound();
            devises.Remove(devise);
            return devise;
        }
    }
}

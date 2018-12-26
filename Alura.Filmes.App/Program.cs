using Alura.Filmes.App.Dados;
using System;
using System.Linq;
using Alura.Filmes.App.Extensions;
using Alura.Filmes.App.Negocio;
using Microsoft.EntityFrameworkCore;

namespace Alura.Filmes.App
{
    class Program
    {
        static void Main(string[] args)
        {
            //select * from actor
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                //CreateAtor(contexto);

                //GetAtores(contexto);

                //GetFilmes(contexto);

                //GetFullFilme(contexto);

                //GetFilmesPorCategoria(contexto);

                //GetIdiomas(contexto);

                GetFilmesPorIdioma(contexto);

                //GetCategorias(contexto);
            }

            //Console.ReadKey();
        }

        private static void GetFilmesPorIdioma(AluraFilmesContexto contexto)
        {
            var idiomas = contexto.Idiomas
                .Include(i => i.FilmesFalados);

            foreach(var idioma in idiomas)
            {
                Console.WriteLine(idioma);

                Console.WriteLine("Filmes Falados:");
                foreach(var filme in idioma.FilmesFalados)
                {
                    Console.WriteLine("\t"+filme);
                }
            }
        }

        private static void GetIdiomas(AluraFilmesContexto contexto)
        {
            foreach (var idioma in contexto.Idiomas)
            {
                Console.WriteLine(idioma);
            }
        }

        private static void GetCategorias(AluraFilmesContexto contexto)
        {
            foreach (var categoria in contexto.Categorias)
            {
                Console.WriteLine(categoria);
            }
        }

        private static void GetFullFilme(AluraFilmesContexto contexto)
        {
            var filmes = contexto.Filmes
                .Include(f => f.Atores)
                .ThenInclude(fa => fa.Ator)
                .Include(f => f.Categorias)
                .ThenInclude(c => c.Categoria);

            Console.WriteLine($"Listando filmes. Total: {filmes.Count()}");

            foreach (var filme in filmes)
            {
                Console.WriteLine(filme);
                Console.WriteLine("Elenco:");

                foreach (var ator in filme.Atores)
                {
                    Console.WriteLine("\t" + ator.Ator);
                }

                Console.WriteLine("Categorias:");

                foreach (var categoria in filme.Categorias)
                {
                    Console.WriteLine("\t" + categoria.Categoria);
                }

                Console.WriteLine("\n*******************************\n");
            }


        }

        private static void GetFilmesPorCategoria(AluraFilmesContexto contexto)
        {
            var categorias = contexto.Categorias
                    .Include(c => c.Filmes)
                    .ThenInclude(f => f.Filme);

            foreach (var c in categorias)
            {
                Console.WriteLine("");
                Console.WriteLine($"Filmes da categoria {c}:");
                foreach (var fc in c.Filmes)
                {
                    Console.WriteLine("\t"+fc.Filme);
                }
            }
        }

        private static void GetFilmes(AluraFilmesContexto contexto)
        {
            var filmes = contexto.Filmes
                .OrderBy(a => a.Titulo)
                .Take(10);

            foreach (var filme in filmes)
            {
                Console.WriteLine(filme);
            }
        }

        private static void GetElenco(AluraFilmesContexto contexto)
        {
            var elenco = contexto.Elenco;
                
            foreach (var e in elenco)
            {
                Console.WriteLine(e);
            }
        }

        private static void GetAtores(AluraFilmesContexto contexto)
        {
            var atores = contexto.Atores
                                .OrderByDescending(a => EF.Property<DateTime>(a, "last_update"))
                                .Take(10);

            foreach (var ator in atores)
            {
                System.Console.WriteLine(ator + " - " + contexto.Entry(ator).Property("last_update").CurrentValue);
            }
        }

        private static void CreateAtor(AluraFilmesContexto contexto)
        {
            var ator = new Ator()
            {
                PrimeiroNome = "James",
                UltimoNome = "Ford"
            };

            contexto.Atores.Add(ator);
            contexto.SaveChanges();
        }
    }
}
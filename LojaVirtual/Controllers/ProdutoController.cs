using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LojaVirtual.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        [HttpGet]
        public ActionResult Index()
        {
            ListagemProdutosEntities1 banco = new ListagemProdutosEntities1();

            var produtos = banco.Produtos.ToList(); 
            return View(produtos);
        }
        public ActionResult Details(int id)
        {
            var produtos = new ListagemProdutosEntities1().Produtos.Where(x => x.ProdutoID == id).SingleOrDefault();
            return View(produtos);
        }
        public ActionResult Delete(int id)
        {
            //estancia a classe do banco
            ListagemProdutosEntities1 banco = new ListagemProdutosEntities1();

            //busca 
            var clientes = banco.Produtos.Where(x => x.ProdutoID == id).SingleOrDefault();

            //apaga
            banco.Produtos.Remove(clientes);

            //salva o banco de dados
            banco.SaveChanges();

            //abre a tela de listar
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Create()
        {
            ListagemProdutosEntities1 banco = new ListagemProdutosEntities1();

            var produtos = banco.Produtos.ToList();

            var lista = new List<SelectListItem>();

            foreach (var prod in produtos)
            {
                lista.Add(new SelectListItem { Value = prod.ProdutoID.ToString(), Text = prod.Nome });
            }
            ViewBag.IDCidade = lista;

            return View();
        }
        public ActionResult Edit(int id)
        {
            ListagemProdutosEntities1 banco = new ListagemProdutosEntities1();

            var produtos = banco.Produtos.Where(w => w.ProdutoID == id).SingleOrDefault();

            var categorias   = banco.Categorias.ToList();

            var lista = new List<SelectListItem>();

            foreach (var cat in categorias)
            {
                lista.Add(new SelectListItem { Value = cat.CategoriaID.ToString(), Text = cat.Nome, Selected = cat.CategoriaID == produtos.IDCategoria });
            }

            ViewBag.IDCidade = lista;

            return View(produtos);
        }

        [HttpPost]
        public ActionResult Edit(Produto produtos)
        { 
            ListagemProdutosEntities1 banco = new ListagemProdutosEntities1();

            var produtoDB = banco.Produtos.Where(w => w.ProdutoID == produtos.ProdutoID).SingleOrDefault();

            produtoDB.ProdutoID = produtoDB.ProdutoID;
            produtoDB.IDCategoria = produtoDB.IDCategoria;
            produtoDB.Nome = produtoDB.Nome;
            produtoDB.Descricao = produtoDB.Descricao;
            produtoDB.Valor = produtoDB.Valor;
            produtoDB.ValorAtual = produtoDB.ValorAtual;

            banco.SaveChanges();

            return RedirectToAction("Details", new { id = produtoDB.ProdutoID });
        }

    }
}
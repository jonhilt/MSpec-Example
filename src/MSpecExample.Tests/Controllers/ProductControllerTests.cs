using System.Collections.Generic;
using System.Web.Mvc;
using Machine.Specifications;
using MSpecExample.Web.Controllers;
using MSpecExample.Web.Models;
using MSpecExample.Web.Repositories;
using Rhino.Mocks;
using StructureMap.AutoMocking;

namespace MSpecExample.Tests.Controllers
{
    [Subject("Product Search")]
    public class when_product_search_page_requested : concern_for_product_controller
    {
        static ActionResult _result;

        Because of =
            () => { _result = _controller.Search(); };

        It should_return_product_search_page =
            () => _result.is_a_view_and().ViewName.ShouldEqual("Search");
    }

    [Subject("Product Search")]
    public class when_asked_for_products_matching_search_term : concern_for_product_controller
    {
        static List<Product> _products;
        static ActionResult _result;

        Establish context =
            () =>
                {
                    _products = new List<Product>();
                    _productRepository.Stub(x => x.FindProducts("test")).Return(_products);
                };

        Because of =
            () => { _result = _controller.Search("test"); };

        It should_retrieve_a_list_of_products_with_titles_containing_the_search_term =
            () => _productRepository.AssertWasCalled(x => x.FindProducts("test"));

        It should_return_the_list_of_products_to_the_user =
            () => _result.is_a_view_and().ViewData.Model.ShouldEqual(_products);

        It should_return_the_search_results_page_to_the_user =
            () => _result.is_a_view_and().ViewName.ShouldEqual("SearchResults");
    }

    [Subject("Product Search")]
    public class when_empty_search_term_entered
    {
        It should_return_an_error_message;
    }

    public class concern_for_product_controller
    {
        protected static ProductController _controller;
        protected static IProductRepository _productRepository;
        static RhinoAutoMocker<ProductController> mocker;

        Establish context =
            () =>
                {
                    mocker = new RhinoAutoMocker<ProductController>();
                    _controller = mocker.ClassUnderTest;
                    _productRepository = mocker.Get<IProductRepository>();
                };
    }
}
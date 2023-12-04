import React from "react";
import { FaCircleXmark } from "react-icons/fa6";

import { IVariant } from "../../interfaces/iVariant";
import { VariantService } from "../../services/variantServices";
import { CategoryService } from "../../services/categoryServices";

import Form from "./form";
import { ECommand } from "../../enums/eCommand";
import { ICategory } from "../../interfaces/iCategory";
import { IPagination } from "../../interfaces/iPagination";
import { IProduct } from "../../interfaces/iProduct";
import { ProductService } from "../../services/productServices";
import { config } from "../../configurations/config";
import GalleryGrid from "../gallery/galleryGrid";

interface IProps {}

interface IState {
  categories: ICategory[];
  variants: IVariant[];
  products: IProduct[];
  product: IProduct;
  command: ECommand;
  showModal: boolean;
  pagination: IPagination;
  noImage: any;
  showGallery: boolean;
}

export default class Products extends React.Component<IProps, IState> {
  newPagination: IPagination = {
    pageNum: 1,
    rows: config.rowsPerPage[0],
    search: "",
    orderBy: "id",
    sort: 1,
    pages: 0,
  };

  newCategory: ICategory = {
    id: 0,
    initial: "",
    name: "",
    active: false,
  };

  newVariant: IVariant = {
    id: 0,
    categoryId: 0,
    initial: "",
    name: "",
    active: false,
    category: this.newCategory,
  };

  newProduct: IProduct = {
    id: 0,
    categoryId: 0,
    variantId: 0,
    initial: "",
    name: "",
    active: false,
    variant: this.newVariant,
    galleryId: 0,
    base64: "",
    description: "",
    price: 0,
    stock: 0,
  };

  constructor(props: IProps) {
    super(props);
    this.state = {
      categories: [],
      variants: [],
      products: [],
      product: this.newProduct,
      showModal: false,
      command: ECommand.create,
      pagination: this.newPagination,
      noImage: config.noImage,
      showGallery: false,
    };
  }

  componentDidMount(): void {
    this.loadProducts();
  }

  loadVariants = async () => {
    const { pagination } = this.state;
    const result = await VariantService.getAll();
    if (result.success) {
      const catResult = await CategoryService.getAll(pagination);
      if (catResult.success) {
        this.setState({
          categories: catResult.result,
        });
      }
      this.setState({
        variants: result.result,
      });
    } else {
      alert("Error:" + result.result);
    }
  };

  loadProducts = async () => {
    const { pagination } = this.state;
    await ProductService.getPagination(pagination).then((result) => {
      console.log(result);
      if (result.success) {
        CategoryService.getAll(pagination).then((catResult) => {
          console.log(catResult);
          if (catResult.success) {
            this.setState({
              categories: catResult.result,
            });
          }
          console.log(result.result);
          this.setState({
            products: result.result,
          });
        });
      } else {
        alert("Error: " + result.result);
      }
    });
  };

  setShowModal = (val: boolean) => {
    this.setState({
      showModal: val,
      showGallery: val,
    });
    console.log(this.state.showModal);
  };

  changeHandler = (name: any) => (event: any) => {
    if (name === "categoryId") {
      VariantService.getByParentId(event.target.value).then((result) => {
        this.setState({
          variants: result.result,
        });
      });
    }
    this.setState({
      product: {
        ...this.state.product,
        [name]: event.target.value,
      },
    });
  };

  checkBoxHandler = (name: any) => (event: any) => {
    this.setState({
      product: {
        ...this.state.product,
        [name]: event.target.checked,
      },
    });
  };

  createCommand = () => {
    this.setState({
      showModal: true,
      product: this.newProduct,
      command: ECommand.create,
    });
    // this.setShowModal(true);
    console.log(this.state.categories, this.state.variants);
  };

  updateCommand = async (id: number) => {
    await ProductService.getById(id)
      .then((result) => {
        if (result.success) {
          this.setState({
            showModal: true,
            product: result.result,
            command: ECommand.edit,
          });
        } else {
          alert("Error result" + result.result);
        }
      })
      .catch((error) => {
        alert("Error" + error);
      });
  };

  openGallery = async (productId: number) => {
    this.setState({
      showGallery: true,
      product: {
        ...this.state.product,
        id: productId,
      },
    });
  };

  selectGalery = async (galleryId: number) => {
    const { id } = this.state.product;
    ProductService.changeGallery(id, galleryId)
      .then((result) => {
        if (result.success) {
          this.setState({
            showGallery: false,
          });
          this.loadProducts();
        } else {
          alert("Error result " + result.result);
        }
      })
      .catch((error) => {
        console.log(error);
      });
  };

  changeStatusCommand = async (id: number) => {
    await ProductService.getById(id)
      .then((result) => {
        if (result.success) {
          this.setState({
            showModal: true,
            product: result.result,
            command: ECommand.changeStatus,
          });
          this.loadProducts();
        } else {
          alert("Error result" + result.result);
        }
      })
      .catch((error) => {
        alert("Error result" + error);
      });
  };

  changeRowPerPage = (name: any) => (event: any) => {
    this.setState({
      pagination: {
        ...this.state.pagination,
        [name]: event.target.value,
      },
    });
    new Promise(() => {
      setTimeout(() => {
        this.loadProducts();
      }, 500);
    });
  };

  changeSearch = (name: any) => (event: any) => {
    this.setState({
      pagination: {
        ...this.state.pagination,
        [name]: event.target.value,
      },
    });
  };

  changeOrder = (column: string) => {
    this.setState({
      pagination: {
        ...this.state.pagination,
        orderBy: column,
      },
    });
    new Promise(() => {
      setTimeout(() => {
        this.loadProducts();
      }, 500);
    });
  };

  submitHandler = async () => {
    const { command, product } = this.state;
    if (command === ECommand.create) {
      await ProductService.post(this.state.product)
        .then((result) => {
          if (result.success) {
            this.setState({
              showModal: false,
              product: this.newProduct,
            });
            this.loadProducts();
          } else {
            alert("Error result " + result.result);
          }
        })
        .catch((error) => {
          alert("Error error" + error);
        });
    } else if (command == ECommand.edit) {
      await ProductService.update(product.id, product)
        .then((result) => {
          if (result.success) {
            this.setState({
              showModal: false,
              product: this.newProduct,
            });
            this.loadProducts();
          } else {
            alert("Error result " + result.result);
          }
        })
        .catch((error) => {
          alert("Error result" + error);
        });
    } else if (command === ECommand.changeStatus) {
      await ProductService.changeStatus(product.id, product.active)
        .then((result) => {
          if (result.success) {
            this.setState({
              showModal: false,
              product: this.newProduct,
            });
            this.loadProducts();
          } else {
            alert("Error result " + result.result);
          }
        })
        .catch((error) => {
          alert("Error error" + error);
        });
    }
  };

  render() {
    const {
      variants,
      categories,
      products,
      product,
      showModal,
      showGallery,
      command,
      noImage,
      pagination,
    } = this.state;
    const loopPages = () => {
      let content: any = [];
      for (let page = 1; page <= pagination.pages; page++) {
        content.push(<option value={page}>{page}</option>);
      }
      return content;
    };
    return (
      <div>
        <div className="text-left text-3xl pt-5">Products</div>
        <span>{JSON.stringify(product)}</span>
        <div className="flex" aria-label="Button">
          <button
            className="my-8 justify-start h-8 px-4 text-green-100 transition-colors duration-150 bg-green-700 rounded focus:shadow-outline hover:bg-green-800"
            onClick={() => this.createCommand()}
          >
            Create New
          </button>
        </div>
        <table className="w-full text-sm text-left text-gray-500 dark:text-gray-400">
          <thead className="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
            <tr className="border-b dark:bg-gray-900 dark:border-gray-700">
              <th colSpan={1} scope="col" className="px-6 py-3 w-14 h-14">
                {/* grow flex-none  */}
                Search
              </th>
              <th colSpan={4} scope="col" className="px-6 py-3 w-14 h-14">
                <input
                  type="text"
                  id="search"
                  className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                  required
                  value={pagination.search}
                  onChange={this.changeSearch("search")}
                />
              </th>
              <th colSpan={1} scope="col" className="px-6 py-3 w-14 h-14">
                <button
                  className="h-8 px-4 text-green-100 transition-colors duration-150 bg-green-700 rounded-l-lg rounded-r-lg focus:shadow-outline hover:bg-green-800"
                  onClick={() => this.loadProducts()}
                >
                  Filter
                </button>
              </th>
              <th colSpan={3} scope="col" className="px-6 py-3 w-14 h-14">
                <select
                  id="countries"
                  className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                  onChange={this.changeRowPerPage("sort")}
                >
                  <option value="0">Asc</option>
                  <option value="1">Desc</option>
                </select>
              </th>
            </tr>
            <tr className="border-b dark:bg-gray-900 dark:border-gray-700">
              <th scope="col" className="px-6 py-3 w-14 h-14">
                Image
              </th>
              <th scope="col" className="px-6 py-3 w-14 h-14">
                Category/Variants
              </th>
              <th scope="col" className="px-6 py-3 w-14 h-14">
                Initial/Name
              </th>
              <th scope="col" className="px-6 py-3 w-14 h-14">
                Description
              </th>
              <th
                scope="col"
                className="px-6 py-3 w-14 h-14"
                onClick={() => this.changeOrder("order")}
              >
                Price
              </th>
              <th
                scope="col"
                className="px-6 py-3 w-14 h-14"
                onClick={() => this.changeOrder("stock")}
              >
                Stock
              </th>
              <th scope="col" className="px-6 py-3 w-14 h-14">
                Active
              </th>
              <th scope="col" className="px-6 py-3 w-14 h-14">
                Action
              </th>
            </tr>
          </thead>
          <tbody>
            {products?.map((prod: IProduct) => {
              return (
                <tr
                  key={prod.id}
                  className="border-b dark:bg-gray-800 dark:border-gray-700"
                >
                  <td
                    className="px-6 py-4"
                    onClick={() => this.openGallery(prod.id)}
                  >
                    <img src={prod.base64 ? prod.base64 : noImage} />
                  </td>

                  <td className="px-6 py-4">
                    {prod.variant?.category.initial}/{prod.variant?.initial}
                  </td>
                  <td className="px-6 py-4">
                    {prod.initial}/{prod.name}
                  </td>
                  <td className="px-6 py-4">{prod.description}</td>
                  <td className="px-6 py-4">{prod.price}</td>
                  <td className="px-6 py-4">{prod.stock}</td>
                  <td className="px-6 py-4">
                    <div className="flex items-center">
                      <input
                        checked={prod.active}
                        id="checked-checkbox"
                        type="checkbox"
                        className="w-4 h-4 text-blue-600 bg-gray-100 border-gray-300 rounded focus:ring-blue-500 dark:focus:ring-blue-600 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600"
                      />
                    </div>
                  </td>
                  <td className="px-4 py-4">
                    <div
                      className="inline-flex"
                      role="group"
                      aria-label="Button group"
                    >
                      <button
                        className="h-8 px-4 text-green-100 transition-colors duration-150 bg-green-700 rounded-l-lg focus:shadow-outline hover:bg-green-800"
                        onClick={() => this.updateCommand(prod.id)}
                      >
                        Edit
                      </button>
                      <button
                        className="h-8 px-4 text-blue-100 transition-colors duration-150 bg-blue-700 rounded-r-lg focus:shadow-outline hover:bg-blue-800"
                        onClick={() => this.changeStatusCommand(prod.id)}
                      >
                        Status
                      </button>
                    </div>
                  </td>
                </tr>
              );
            })}
          </tbody>
          <tfoot className="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
            <tr>
              <th scope="col" className="px-6 py-3 w-14 h-14">
                <label className="block text-sm font-medium text-gray-900 dark:text-white">
                  Rows per page
                </label>
              </th>
              <th scope="col" className="px-6 py-3 w-14 h-14">
                <select
                  id="countries"
                  className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                  onChange={this.changeRowPerPage("rows")}
                >
                  {config.rowsPerPage.map((o: number) => {
                    return <option value={o}>{o}</option>;
                  })}
                </select>
              </th>
              <th scope="col" className="px-6 py-3 w-14 h-14"></th>
              <th scope="col" className="px-6 py-3 w-14 h-14"></th>
              <th scope="col" className="px-6 py-3 w-14 h-14"></th>
              <th scope="col" className="px-6 py-3 w-14 h-14">
                Page:
              </th>
              <th colSpan={3} scope="col" className="px-6 py-3 w-14 h-14">
                <select
                  id="countries"
                  className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                  onChange={this.changeRowPerPage("pageNum")}
                >
                  {loopPages()}
                </select>
              </th>
            </tr>
          </tfoot>
        </table>
        {showModal ? (
          <div className="fixed inset-0 z-50 overflow-hidden">
            <div className="flex justify-center items-center min-h-screen px-4 pt-4 pb-20 text-center">
              <div className="w-full max-w-md bg-white dark:bg-gray-900 rounded-lg shadow-lg overflow-hidden overflow-y-auto">
                <div className="flex items-start justify-between p-5 border-b border-gray-300 dark:border-gray-700 rounded-t">
                  <h3 className="text-3xl text-gray-900 dark:text-white">
                    {command.valueOf()}
                  </h3>
                  <button
                    className="text-black float-right"
                    onClick={() => this.setShowModal(false)}
                  >
                    <span className="text-black opacity-70 h-6 w-6 text-xl block bg-gray-400 dark:bg-gray-700 py-0 rounded-full">
                      ×
                    </span>
                  </button>
                </div>
                <div className="p-6 overflow-y-auto max-h-96">
                  {/* Adjust max-h-96 to the maximum height you want before scrolling starts */}
                  <Form
                    categories={categories}
                    variants={variants}
                    product={product}
                    command={command}
                    changeHandler={this.changeHandler}
                    checkBoxHandler={this.checkBoxHandler}
                  />
                </div>
                <div
                  className="flex items-center justify-end p-6 border-t border-solid border-blueGray-200 rounded-b"
                  role="group"
                  aria-label="Button group"
                >
                  <button
                    className="my-8 justify-start h-8 px-4 text-green-100 transition-colors duration-150 bg-green-700 rounded-l-lg focus:shadow-outline hover:bg-green-800"
                    onClick={() => this.setShowModal(false)}
                  >
                    Close
                  </button>
                  <button
                    className="my-8 justify-start h-8 px-4 text-blue-100 transition-colors duration-150 bg-blue-700 rounded-r-lg focus:shadow-outline hover:bg-blue-800"
                    onClick={() => this.submitHandler()}
                  >
                    Submit
                  </button>
                </div>
              </div>
            </div>
          </div>
        ) : null}
        {showGallery ? (
          <div className="fixed top-0 left-0 w-full h-full flex items-center justify-center bg-gray-800 bg-opacity-75">
            <div className="bg-white p-5 rounded-lg max-w-md w-full max-h-full overflow-y-auto">
              <div className="text-3xl text-slate-800 items-center flex justify-between">
                Gallery
                <FaCircleXmark
                  className="hover:cursor-pointer focus:cursor-default"
                  onClick={() => this.setShowModal(false)}
                />
              </div>

              <div className="relative p-6 flex-auto">
                <GalleryGrid selectGalery={this.selectGalery} />
              </div>
            </div>
          </div>
        ) : null}
      </div>
    );
  }
}

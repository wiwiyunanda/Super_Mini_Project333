import React from "react";

import { ICategory } from "../../interfaces/iCategory";
import { CategoryService } from "../../services/categoryServices";

import Form from "./form";
import { ECommand } from "../../enums/eCommand";
import { IPagination } from "../../interfaces/iPagination";
import { config } from "../../configurations/config";

interface IProps {}

interface IState {
  categories: ICategory[];
  category: ICategory;
  showModal: boolean;
  command: ECommand;
  pagination: IPagination;
}

export default class Category extends React.Component<IProps, IState> {
  newPagination: IPagination = {
    pageNum: 1,
    rows: config.rowsPerPage[0],
    search: "",
    orderBy: "id",
    sort: 1,
    pages: 0,
  };
  constructor(props: IProps) {
    super(props);
    this.state = {
      categories: [],
      category: {
        id: 0,
        initial: "",
        name: "",
        active: false,
      },
      showModal: false,
      command: ECommand.create,
      pagination: this.newPagination,
    };
  }

  componentDidMount(): void {
    this.loadCategories();
  }
  loadCategories = async () => {
    const { pagination } = this.state;
    const result = await CategoryService.getAll(pagination);
    if (result.success) {
      this.setState({
        categories: result.result,
        pagination: {
          ...this.state.pagination,
          pages: result.pages,
        },
      });
      console.log(result);
    } else {
      alert("Error: " + result.result);
    }
  };

  setShowModal = (val: boolean) => {
    this.setState({
      showModal: val,
    });
    console.log(this.state.showModal);
  };

  changeHandler = (name: any) => (event: any) => {
    this.setState({
      category: {
        ...this.state.category,
        [name]: event.target.value,
      },
    });
  };

  checkBoxHandler = (name: any) => (event: any) => {
    this.setState({
      category: {
        ...this.state.category,
        [name]: event.target.checked,
      },
    });
  };

  createCommand = () => {
    this.setState({
      showModal: true,
      category: { id: 0, initial: "", name: "", active: false },
      command: ECommand.create,
    });
    //this.setShowModal(true);
  };
  updateCommand = async (id: number) => {
    await CategoryService.getById(id)
      .then((result) => {
        if (result.success) {
          this.setState({
            showModal: true,
            category: result.result,
            command: ECommand.edit,
          });
          this.loadCategories();
        } else {
          alert("error" + result.result);
        }
      })
      .catch((error) => {
        alert("error" + error);
      });
  };

  changeStatusCommand = async (id: number) => {
    await CategoryService.getById(id)
      .then((result) => {
        if (result.success) {
          this.setState({
            showModal: true,
            category: result.result,
            command: ECommand.changeStatus,
          });
        } else {
          alert("Error result " + result.result);
        }
      })
      .catch((error) => {
        alert("Error error" + error);
      });
  };

  changeRowsPerPage = (name: any) => (event: any) => {
    this.setState({
      pagination: {
        ...this.state.pagination,
        [name]: event.target.value,
      },
    });
    new Promise(() => {
      setTimeout(() => {
        this.loadCategories();
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
        this.loadCategories();
      }, 500);
    });
  };

  submitHandler = async () => {
    const { category } = this.state;
    const { command } = this.state;
    if (command == ECommand.create) {
      await CategoryService.post(this.state.category)
        .then((result) => {
          if (result.success) {
            this.setState({
              showModal: false,
              category: {
                id: 0,
                initial: "",
                name: "",
                active: true,
              },
            });
            this.loadCategories();
          } else {
            alert("Error result" + result.result);
          }
        })
        .catch((error) => {
          alert("Error error" + error);
        });
    } else if (command == ECommand.edit) {
      await CategoryService.update(category.id, category)
        .then((result) => {
          if (result.success) {
            this.setState({
              showModal: false,
              category: {
                id: 0,
                initial: "",
                name: "",
                active: true,
              },
            });
            this.loadCategories();
          } else {
            alert("Error result" + result.result);
          }
        })
        .catch((error) => {
          alert("Error error" + error);
        });
    } else if (command == ECommand.changeStatus) {
      await CategoryService.changeStatus(category.id, category.active)
        .then((result) => {
          if (result.success) {
            this.setState({
              showModal: false,
              category: {
                id: 0,
                initial: "",
                name: "",
                active: true,
              },
            });
            this.loadCategories();
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
    const { categories, category, showModal, command, pagination } = this.state;
    const loopPages = () => {
      let content: any = [];
      for (let page = 1; page <= pagination.pages; page++) {
        content.push(<option value={page}>{page}</option>);
      }
      return content;
    };
    return (
      <div>
        <div className="text-left text-3xl pt-5 text-center">Categories</div>
        <span>{JSON.stringify(category)}</span>
        <div className="flex" aria-label="Button">
          <button
            className="my-8 justify-start h-8 px-4 text-green-100 transition-colors duration-150 
                    bg-green-700 rounded focus:shadow-outline hover:bg-green-800"
            onClick={() => this.createCommand()}
          >
            Create New{" "}
          </button>
        </div>
        <table className="w-full text-sm text-left  text-gray-50">
          <thead className="text-xs text-gray-300 uppercase bg-gray-50">
            <tr className="border-b bg-gray-700 border-gray-700">
              <th scope="col" className="px-6 py-3 w-14 h-14">
                Search:
              </th>
              <th colSpan={1} scope="col" className="px-6 py-3 w-14 h-14">
                <input
                  readOnly={command == ECommand.changeStatus}
                  type="text"
                  id="search"
                  className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                  required
                  value={pagination.search}
                  onChange={this.changeSearch("search")}
                />
              </th>
              <th>
                <button
                  className="my-8 justify-start h-8 px-4 text-green-100 transition-colors duration-150 
                    bg-green-700 rounded focus:shadow-outline hover:bg-green-800"
                  onClick={() => this.loadCategories()}
                >
                  Filter
                </button>
              </th>
              <th scope="col" className="px-6 py-3 w-14 h-14">
                <select
                  id="countries"
                  className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                  onChange={this.changeRowsPerPage("sort")}
                >
                  <option value="0">Asc</option>
                  <option value="1">Desc</option>
                </select>
              </th>
            </tr>
            <tr className="border-b bg-gray-700 border-gray-700">
              <th
                scope="col"
                className="px-6 py-3 w-14 h-14"
                onClick={() => this.changeOrder("initial")}
              >
                Initial
              </th>
              <th
                scope="col"
                className="px-6 py-3 w-14 h-14"
                onClick={() => this.changeOrder("name")}
              >
                Name
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
            {categories?.map((cat: ICategory) => {
              return (
                <tr
                  key={cat.id}
                  className="border-b dark:bg-gray-800 dark:border-gray-700"
                >
                  <td className="px-6 py-4">{cat.initial}</td>
                  <td className="px-6 py-4">{cat.name}</td>
                  <td className="px-6 py-4">
                    <div className="flex items-center">
                      <input
                        checked={cat.active}
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
                        onClick={() => this.updateCommand(cat.id)}
                      >
                        Edit
                      </button>
                      <button
                        className="h-8 px-4 text-blue-100 transition-colors duration-150 bg-blue-700 rounded-r-lg focus:shadow-outline hover:bg-blue-800"
                        onClick={() => this.changeStatusCommand(cat.id)}
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
            <tr className="border-b dark:bg-gray-900 dark:border-gray-700">
              <th scope="col" className="px-6 py-3 w-14 h-14">
                <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">
                  Rows per page:
                </label>
              </th>
              <th scope="col" className="px-6 py-3 w-14 h-14">
                <select
                  id="countries"
                  className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                  onClick={this.changeRowsPerPage("rows")}
                >
                  {config.rowsPerPage.map((cat: number) => {
                    return <option value={cat}>{cat}</option>;
                  })}
                </select>
              </th>
              <th scope="col" className="px-6 py-3 w-14 h-14">
                Page:
              </th>
              <th scope="col" className="px-6 py-3 w-14 h-14">
                <select
                  id="countries"
                  className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                  onClick={this.changeRowsPerPage("pageNum")}
                >
                  {loopPages()}
                </select>
              </th>
            </tr>
          </tfoot>
        </table>
        {showModal ? (
          <div className="flex justify-center items-center overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none ">
            <div className="relative w-auto my-6 mx-auto max-w-3xl ">
              <div className="border-0 rounded-lg shadow-lg relative flex flex-col w-full bg-white outline-none focus:outline-none dark:bg-gray-900">
                <div className="flex items-start justify-between p-5 border-b border-solid border-gray-300 rounded-t ">
                  <h3 className="text-3xl text-gray-900 dark:text-white">
                    {command.valueOf()}
                  </h3>
                  <button
                    className="bg-transparent border-0 text-black float-right"
                    onClick={() => this.setShowModal(false)}
                  >
                    <span className="text-black opacity-7 h-6 w-6 text-xl block bg-gray-400 py-0 rounded-full">
                      x
                    </span>
                  </button>
                </div>
                <div className="relative p-6 flex-auto">
                  <Form
                    category={category}
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
      </div>
    );
  }
}

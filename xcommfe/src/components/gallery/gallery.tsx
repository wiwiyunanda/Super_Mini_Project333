import React from "react";

import { IGallery } from "../../interfaces/iGallery";
import { GalleryService } from "../../services/galleryService";
import Form from "./form";
import { ECommand } from "../../enums/eCommand";
import { IPagination } from "../../interfaces/iPagination";

import { config } from "../../configurations/config";

interface IProps {}

interface IState {
  galleries: IGallery[];
  gallery: IGallery;
  showModal: boolean;
  command: ECommand;
  pagination: IPagination;
}

export default class Gallery extends React.Component<IProps, IState> {
  newPagination: IPagination = {
    pageNum: 1,
    rows: config.rowsPerPage[0],
    search: "",
    orderBy: "id",
    sort: 1,
    pages: 0,
  };

  newGallery: IGallery = {
    id: 0,
    title: "",
    base64Big: "",
    base64Small: "",
    active: false,
  };

  constructor(props: IProps) {
    super(props);
    this.state = {
      galleries: [],
      gallery: this.newGallery,
      showModal: false,
      command: ECommand.create,
      pagination: this.newPagination,
    };
  }

  componentDidMount(): void {
    this.loadGalleries();
  }

  loadGalleries = async () => {
    const { pagination } = this.state;
    const result = await GalleryService.getAll(pagination);
    if (result.success) {
      this.setState({
        galleries: result.result,
        pagination: {
          ...this.state.pagination,
          pages: result.pages,
        },
      });
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
      gallery: {
        ...this.state.gallery,
        [name]: event.target.value,
      },
    });
  };

  checkBoxHandler = (name: any) => (event: any) => {
    this.setState({
      gallery: {
        ...this.state.gallery,
        [name]: event.target.checked,
      },
    });
  };

  createCommand = () => {
    this.setState({
      showModal: true,
      gallery: this.newGallery,
      command: ECommand.create,
    });
    // this.setShowModal(true);
  };

  photoUpload = (event: any) => {
    const file = event.target.files[0];

    const reader = new FileReader();
    reader.readAsDataURL(file);

    reader.onload = () => {
      // const img = new Image();
      // img.src = event.target.result.toString();
      this.resizeImage(reader.result, 512, 512)
          .then(result => {
              this.setState({
                  gallery: {
                      ...this.state.gallery,
                      base64Big: result,
                  }
              })
              this.resizeImage(reader.result, 128, 128)
                  .then(resultSmall => {
                      this.setState({
                          gallery: {
                              ...this.state.gallery,
                              base64Small: resultSmall
                          }
                      })
                      console.log(result, resultSmall);
                  }).catch(error => {
                      alert(error);
                  })
          }).catch(error => {
              alert(error);
          })

  };


    reader.onerror = (error) => {
      console.log("Error: ", error);
    };
  };

  resizeImage = (base64Str: any, maxWidth: number = 512, maxHeight: number = 512) => {
    return new Promise((resolve) => {
        let img = new Image()
        img.src = base64Str
        img.onload = () => {
            let canvas = document.createElement('canvas')
            const MAX_WIDTH = maxWidth
            const MAX_HEIGHT = maxHeight
            let width = img.width
            let height = img.height

            if (width > height) {
                if (width > MAX_WIDTH) {
                    height *= MAX_WIDTH / width
                    width = MAX_WIDTH
                }
            } else {
                if (height > MAX_HEIGHT) {
                    width *= MAX_HEIGHT / height
                    height = MAX_HEIGHT
                }
            }
            canvas.width = width
            canvas.height = height
            let ctx: any = canvas.getContext('2d')
            ctx.drawImage(img, 0, 0, width, height)
            resolve(canvas.toDataURL())
        }
    })
  }

  submitHandler = async () => {
    const { command, gallery } = this.state;
    if (command == ECommand.create) {
      await GalleryService.post(this.state.gallery)
        .then((result) => {
          if (result.success) {
            this.setState({
              showModal: false,
              gallery: this.newGallery,
            });
            this.loadGalleries();
          } else {
            alert("Error result " + result.result);
          }
        })
        .catch((error) => {
          alert("Error error" + error);
        });
    } else if (command == ECommand.edit) {
    } else if (command == ECommand.changeStatus) {
    }
  };

  render() {
    const { galleries, gallery, showModal, command, pagination } = this.state;

    return (
      <div>
        <div className="text-left text-3xl pt-5">Galleries</div>
        <span>{JSON.stringify(gallery)}</span>
        <div className="flex" aria-label="Button">
          <button
            className="my-8 justify-start h-8 px-4 text-green-100 transition-colors duration-150 bg-green-700 rounded focus:shadow-outline hover:bg-green-800"
            onClick={this.createCommand}
          >
            Create New
          </button>
        </div>
        <table className="w-full text-sm text-left text-gray-500 dark:text-gray-400">
          <tbody>
            {galleries.map((o: IGallery) => {
              return (
                <tr
                  key={o.id}
                  className="border-b dark:bg-gray-800 dark:border-gray-700"
                >
                  <td className="px-6 py-4">
                    <img height={128} width={128} src={o.base64Big}></img>
                  </td>
                  <td className="px-6 py-4">{o.title}</td>
                  <td className="px-6 py-4">
                    <div className="flex items-center">
                      <input
                        checked={o.active}
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
                      <button className="h-8 px-4 text-green-100 transition-colors duration-150 bg-green-700 rounded-l-lg focus:shadow-outline hover:bg-green-800">
                        Edit
                      </button>
                      <button className="h-8 px-4 text-blue-100 transition-colors duration-150 bg-blue-700 rounded-r-lg focus:shadow-outline hover:bg-blue-800">
                        Status
                      </button>
                    </div>
                  </td>
                </tr>
              );
            })}
          </tbody>
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
                    gallery={gallery}
                    command={command}
                    photoUpload={this.photoUpload}
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

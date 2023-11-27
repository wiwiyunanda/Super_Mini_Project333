import React from 'react';
import { config } from '../../configurations/config';
import { ECommand } from '../../enums/eCommand';
import { IGallery } from '../../interfaces/iGallery';

interface IProps {
    gallery: IGallery;
    command: ECommand;
    changeHandler: any;
    checkBoxHandler: any;
    photoUpload: any;
}

interface IState {
    imagePreview: any
}

export default class Form extends React.Component<IProps, IState> {
    constructor(props: IProps) {
        super(props);
        this.state = {
            imagePreview: config.noImage
        }
    }

    render() {
        const { gallery, command, photoUpload, changeHandler, checkBoxHandler } = this.props;
        const { imagePreview } = this.state;
        return (
            <>
                <form>
                    <div className="mb-6">
                        <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Image</label>
                        <img src={gallery.base64Big ? gallery.base64Big : imagePreview}></img>
                        <input type="file" name='uploadFile' accept='.jpg,.jpeg,.png' onChange={photoUpload} src={imagePreview} />
                    </div>
                    <div className="mb-6">
                        <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Title</label>
                        <input readOnly={command == ECommand.changeStatus} type="text" id="initial" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" required value={gallery.title} onChange={changeHandler('title')} />
                    </div>
                    {
                        command == ECommand.changeStatus ?
                            (
                                <div className="flex items-start mb-6">
                                    <div className="flex items-center h-5">
                                        <input type="checkbox" value="" className="w-4 h-4 border border-gray-300 rounded bg-gray-50 focus:ring-3 focus:ring-blue-300 dark:bg-gray-700 dark:border-gray-600 dark:focus:ring-blue-600 dark:ring-offset-gray-800 dark:focus:ring-offset-gray-800" required checked={gallery.active} onChange={checkBoxHandler('active')} />
                                    </div>
                                    <label className="ms-2 text-sm font-medium text-gray-900 dark:text-gray-700">Is Active</label>
                                </div>
                            ) : null
                    }

                </form>
            </>
        )
    }
}

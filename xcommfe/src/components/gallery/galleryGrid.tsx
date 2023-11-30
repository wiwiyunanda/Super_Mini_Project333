import React from 'react';

import { IGallery } from '../../interfaces/iGallery';
import { GalleryService } from '../../services/galleryService';
import { IPagination } from '../../interfaces/iPagination';

interface IProps {
    selectGalery: any
}

interface IState {
    galleries: IGallery[];
    pagination: IPagination;
}

export default class GalleryGrid extends React.Component<IProps, IState> {
    newPagination: IPagination = {
        pageNum: 1,
        rows: 100,
        search: '',
        orderBy: 'id',
        sort: 1,
        pages: 0
    }
    constructor(props: IProps) {
        super(props);
        this.state = {
            galleries: [],
            pagination: this.newPagination
        }
    }

    componentDidMount(): void {
        this.loadGalleries();
    }

    loadGalleries = async () => {
        const { pagination } = this.state;
        GalleryService.getAll(pagination)
            .then(result => {
                if (result.success) {
                    this.setState({
                        galleries: result.result,
                        pagination: {
                            ...this.state.pagination,
                            pages: result.pages
                        }
                    });
                } else {
                    alert('Error: ' + result.result);
                }
            }).catch(error => {
                console.log(error);
            })
    }

    render() {
        const { galleries } = this.state;
        const { selectGalery } = this.props;
        return (
            <div className="grid grid-cols-2 md:grid-cols-3 gap-4">
                {
                    galleries.map((o: IGallery) => {
                        return (
                            <div>
                                <img className="h-auto max-w-full rounded-lg" src={o.base64Small} onClick={() => selectGalery(o.id)} alt="" />
                            </div>
                        )
                    })
                }
            </div>
        )
    }
}

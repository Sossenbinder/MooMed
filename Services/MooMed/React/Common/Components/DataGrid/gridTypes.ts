export type GridCellProps<T> = {
	rowData: T;
}

export interface GridEntry {
	[key: string]: any;
}

export type GridCellConfig<T extends GridEntry> = {
	customCell?(val: T): JSX.Element;
	key?: keyof T;
	customValueGenerator?(val: T): string;
	cellValueIfNull?: string;
}

export type GridColumn<T extends GridEntry> = {
	headerText?: string;
	cellconfig: GridCellConfig<T>;
}

export type PagingInfo = {
	entriesPerPage: number;	
}

export type GridConfiguration<T extends GridEntry> = {
	columns: Array<GridColumn<T>>;
	pagingInfo?: PagingInfo;
	idField: keyof T;
}
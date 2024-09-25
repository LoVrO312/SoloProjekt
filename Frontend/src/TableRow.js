import React from 'react';
import ActionButtons from './ActionButtons'

function TableRow({ subject }) {
    return (
        <>
            <td>{subject.id}</td>
            <td>{subject.name}</td>
            <td>{subject.ects}</td>
            <td>{subject.departmentId}</td>
            <td>{subject.timeCreated}</td>
        </>
    );
}


export default TableRow;

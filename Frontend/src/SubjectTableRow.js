import React from 'react';
import ActionButtons from './ActionButtons'

function SubjectTableRow({ subject, handleUpdate, handleDelete }) {
    return (
        <tr>
            <td>{subject.id}</td>
            <td>{subject.name}</td>
            <td>{subject.ectsPoints}</td>
            <td>{subject.departmentId}</td>
            <td>{subject.timeCreated}</td>
            <ActionButtons 
                  subject={subject}
                  handleUpdate={handleUpdate}
                  handleDelete={handleDelete}
            />
        </tr>
    );
}


export default SubjectTableRow;

import React from 'react'

function ActionButtons({ subject, handleUpdate, handleDelete }){
    return (
        <td>
            <button type="button" onClick={() => handleUpdate(subject)}>Update</button>
            <button type="button" onClick={() => handleDelete(subject.id)}>Delete</button>
        </td>
    );
}

export default ActionButtons;
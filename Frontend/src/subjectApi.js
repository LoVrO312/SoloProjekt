import axios from 'axios';

const getSubjectById = async (id = "19504c46-9217-4d1a-ac7d-7e41a93ece10") => {
  try {
    const response = await axios.get(`https://localhost:7111/subject/read/${id}`);
    return response.data;
  } catch (error) {
    console.error('Error fetching the subject:', error);
    throw error;
  }
};

const getAllSubjectsFiltered = async ({filter, sort, page}) => {
  try {
    const params = {
      departmentId: filter.departmentId || null,
      minEctsPoints: filter.minEctsPoints || null,
      maxEctsPoints: filter.maxEctsPoints || null,
      fromTimeCreated: filter.fromTimeCreated || null,
      toTimeCreated: filter.toTimeCreated || null,
      recordsPerPage: page.recordsPerPage || 15,
      pageNumber: page.pageNumber || 1,
      sortBy: sort.sortBy || 'Subject Name',
      sortOrder: sort.sortOrder || 'Descending',
      searchQuery: filter.searchQuery || ''
    };

    const response = await axios.get('https://localhost:7111/subject/readallfiltered', {params});
    return response.data;
  } catch (error) {
    console.error('Error fetching filtered subjects:', error);
    throw error;
  }
};

const createSubject = async (subject) => {
    try {
        await axios.post('https://localhost:7111/subject/create', subject);
    } catch (error) {
        console.error('Error inserting subject', error);
        throw error;
    }

}

const deleteSubject = async (id) => {
    try {
        await axios.delete(`https://localhost:7111/subject/delete/${id}`);
    } catch (error) {
        console.error("Unable to delete subject", error);
        throw error;
    }
}

const updateSubjectDepartment = async (id, departmentId) => {
    try {
        await axios.put(`https://localhost:7111/subject/update/${id}/${departmentId}`);
    } catch (error) {
        console.error("unable to update subject's department", error);
        throw error;
    }
}

const getSubjectDepartments = async () => {
  try {
    const response = await axios.get('https://localhost:7111/subject/readcorrespondingdepartments');
    return response.data;
  } catch (error) {
    console.error("unable to fetch subject department data");
    throw error;
  }
}


export { getSubjectById, getAllSubjectsFiltered, createSubject, deleteSubject, updateSubjectDepartment, getSubjectDepartments};

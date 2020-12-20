import axios from 'axios'


const getAllRuleCategories = async ()=>{

    const params = {
        params: {
            appName:'scanf'
        },
      };
    // console.log(process.env.VUE_APP_ID);
    const response = await axios.get(' http://localhost:7071/api/category/getall',params);
    const result = response.data.map(item => item.rowKey);
    return result;
};


const getAllItemsForCategory = async (category)=>{

    const params ={
        params:{
            appName:'scanf',
            categoryName:category
        }
    };

    const response = await axios.get('http://localhost:7071/api/item/getall',params);
    const result = response.data.map(item=>{
        const serializedData = JSON.parse(item.value);

        console.log('sdata='+serializedData)
        console.log(serializedData.title)
        return {
            title: serializedData.title,
            description: serializedData.descriptionPath,
            category: category
        };
    });

    return result;
}

export {
    getAllRuleCategories, getAllItemsForCategory
};
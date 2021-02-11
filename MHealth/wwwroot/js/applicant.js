//var country = $("#country").select2();
//var state = $("#state").select2();
//var city = $("#city").select2();
var apiFilterData = null;

function fillSelectWithData() {
    //$.ajax({
    //    type: "GET",
    //    url: `/Applicant/GetCities`,
    //    dataType: 'JSON',
    //    contentType: "application/json"
    //}).done(function (data) {
    //    apiFilterData = data;
    //    addCountry(data);
    //    //addStates(data);
    //    //addCities(data);
    //}).fail(function (err) {
    //    //alert("error : " + err.responseJSON.message);
    //});

    addCountry(data);

    country.change(function (e) {
        let selected = country.val();
        state.empty();
        //city.empty()
        let filtered = apiFilterData.filter(function (obj) {

            if (selected.length > 0) {
                return selected.includes(obj.country_id);
            } else {
                return apiFilterData;
            }
        });
        addStates(filtered);
        //addCities(filtered);
    });
    state.change(function (e) {

        let selected = state.val();
        //city.empty();
        let filtered = apiFilterData.filter(function (obj) {
            if (selected.length > 0) {
                return selected.includes(obj.state_id);
            } else {
                return apiFilterData;
            }
        });
        //addCities(filtered);
    });
}

function addCountry(d) {
    //clear current
    country.html("");

    //reduce object to just properties we need
    let g = d.map(({
        country_id,
        country_name
    }) => {
        return {
            country_id,
            country_name
        };
    });

    // save current config options
    let options = country.data('select2').options.options;

    options.data = g.reduce((acc, current) => {
        const x = acc.find(item => item.country_id === current.country_id);
        if (!x) {
            country.append("<option value=\"" + current.country_id + "\">" + current.country_name +
                "</option>");
            return acc.concat([current]);
        } else {
            return acc;
        }
    }, []);
    //recreate select2
    country.select2(options);
}

function addStates(d) {

    //clear current
    state.html("");

    //reduce object to just properties we need
    let g = d.map(({
        state_id,
        state_name
    }) => {
        return {
            state_id,
            state_name
        };
    });

    // save current config options
    let options = state.data('select2').options.options;

    options.data = g.reduce((acc, current) => {
        const x = acc.find(item => item.state_id === current.state_id);
        if (!x) {
            state.append("<option value=\"" + current.state_id + "\">" + current.state_name +
                "</option>");
            return acc.concat([current]);
        } else {
            return acc;
        }
    }, []);
    //recreate select2
    state.select2(options);
}

function addCities(d) {

    //clear current
    city.html("");

    //reduce object to just properties we need
    let g = d.map(({
        id,
        name
    }) => {
        return {
            id,
            name
        };
    });

    // save current config options
    let options = city.data('select2').options.options;

    options.data = g.reduce((acc, current) => {
        const x = acc.find(item => item.id === current.name);
        if (!x) {
            city.append("<option value=\"" + current.id + "\">" + current.name + "</option>");
            return acc.concat([current]);
        } else {
            return acc;
        }
    }, []);
    //recreate select2
    city.select2(options);
}
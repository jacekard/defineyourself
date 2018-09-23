from plotly.offline import download_plotlyjs, init_notebook_mode, plot, iplot
from plotly.graph_objs import Scatter, Layout, Figure

def get_data_from_file(file_name):
    file_data = []
    try:
        file = open(file_name, "r+")
        for line in file:
            file_data.append(line)
        file.close()
        return file_data
    except IOError:
        print("Could not open "+file_name)
        return False


def main():
    data_x = get_data_from_file("data/ideas_by_date_x.txt")
    data_y = get_data_from_file("data/ideas_by_date_y.txt")
    
    if not data_x or not data_y:
        return
    
    x_axis = data_x

    # Create a trace
    trace0 = Scatter(
        x=data_x,
        mode='lines',
        y=data_y,
        name='ideas',
        line = dict(color = ('rgb(0, 0,255)'), width=8)
    )

    layout = Layout(
        yaxis=dict(
            title='number of ideas',
            
        ),
        xaxis=dict(
            title='date',
        )
    )
   
    plot(Figure(data=[trace0], layout=layout))

if __name__ == "__main__":
    main()

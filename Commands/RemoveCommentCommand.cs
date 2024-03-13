using Instagram.Interfaces;
using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Commands
{
    public class RemoveCommentCommand<T> : CommandBase where T : ModelBase
    {
        private readonly IBothCommentsRepository<T> _repository;
        private readonly int _id;
        private readonly Action<int> _UpdateCommentsAfterDelete;

        public RemoveCommentCommand(IBothCommentsRepository<T> repository, int id, Action<int> UpdateCommentsAfterDelete)
        {
            _repository = repository;
            _id = id;
            _UpdateCommentsAfterDelete = UpdateCommentsAfterDelete;
        }
        public async override void Execute(object parameter)
        {
            await _repository.DeleteCommentAsync(_id);
            _UpdateCommentsAfterDelete.Invoke(_id);
        }
    }
}
